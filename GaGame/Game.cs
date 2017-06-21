/*
 * Saxion, Game Architecture
 * User: Eelco Jannink
 * Date: 19-5-2016
 * Time: 16:55
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

public enum GameState { GameStart, Running, Paused, GameOver }

public class Game {
    static private Random random = new Random(0); // seed for repeatability.
    private Window _window;
    private GameState _gameState;
    private CollisionManager _collisionManager;

    [STAThread] // needed to use wpf Keyboard.isKeyPressed when single threaded !
    static public void Main() {
        Console.WriteLine("Starting Game, close with Escape");
        Game game;
        game = new Game();
        game.Build();
        game.Run();
        game.Close();
        Console.WriteLine("Closed window");
    } 

	public Game() {
        _window = new Window( this );
        Locator.Game = this;
        _gameState = GameState.GameStart;
        _collisionManager = new CollisionManager(this);
        Locator.CollisionManager = _collisionManager;
        PauseEvent.Handlers += this.PauseHandler;

    }

    //GameObjects
    private GameObject _gameManager;
    private GameObject _ball;
    private GameObject _leftPaddle, _rightPaddle;
    private GameObject _leftScore, _rightScore;
    private GameObject _booster1, _booster2;

    //Lists
    private List<GameObject> gameObjectList = new List<GameObject>();
    private List<RenderComponent> drawableList = new List<RenderComponent>();

    //EventQue (StartQue)
    static int MAX_STARTEVENTS = 100;
    private int _startQueHead = 0;
    private int _startQueTail = 0;
    Component[] componentStartQue = new Component[MAX_STARTEVENTS];

    //GameLoop
    static double MS_PER_UPDATE = 0.016;  //Time 
    double currentUpdateTime;   //Time @ update start
    double previousUpdateTime;  //Time @ previous update start
    
    double lag = 0.0;
    
    private void Build() {
        #region Managers
        _gameManager = new GameObject("GameManager");
        _gameManager.AddComponent<GameManager>();
        #endregion

        #region Ball 
        _ball = new GameObject("Ball");
        _ball.AddComponent<RigidBody>();
        _ball.AddComponent<Collider>();
        _ball.AddComponent<RenderComponent>().Image = Image.FromFile("ball.png");
        _ball.AddComponent<BallScript>();
        #endregion

        #region Paddles
        _leftPaddle = new GameObject("LeftPaddle", new Vec2(10, 208));
        _leftPaddle.AddComponent<RigidBody>();
        _leftPaddle.AddComponent<Collider>();
        _leftPaddle.AddComponent<RenderComponent>().Image = Image.FromFile("paddle.png");
        _leftPaddle.AddComponent<PaddleInput>();
        _leftPaddle.AddComponent<PaddleScript>();

        _rightPaddle = new GameObject("RightPaddle", new Vec2(622, 208));
        _rightPaddle.AddComponent<RigidBody>();
        _rightPaddle.AddComponent<Collider>();
        _rightPaddle.AddComponent<RenderComponent>().Image = Image.FromFile("paddle.png");
        _rightPaddle.AddComponent<PaddleInput>();
        _rightPaddle.AddComponent<PaddleScript>();
        #endregion

        #region Scores
        //LeftScore
        _leftScore = new GameObject("LeftScore", new Vec2(320 - 20 - 66, 10));
        _leftScore.AddComponent<TextComponent>().Paddle = _leftPaddle;
        _leftScore.GetComponent<TextComponent>().Image = Image.FromFile("digits.png");

        //RightScore
        _rightScore = new GameObject("RightScore", new Vec2(320 + 20, 10));
        _rightScore.AddComponent<TextComponent>().Paddle = _rightPaddle;
        _rightScore.GetComponent<TextComponent>().Image = Image.FromFile("digits.png");
        #endregion

        #region Boosters
        //Booster1
        _booster1 = new GameObject("Booster1", new Vec2(304, 96));
        _booster1.AddComponent<RenderComponent>().Image = Image.FromFile("booster.png");
        _booster1.AddComponent<BoosterScript>();
        
        //Booster2
        _booster2 = new GameObject("Booster2", new Vec2(304, 384));
        _booster2.AddComponent<RenderComponent>().Image = Image.FromFile("booster.png");
        _booster2.AddComponent<BoosterScript>();
        #endregion

        Locator.EventManager.DeliverEvents();
    }

    public void Run() {
		Time.Timeout( "Reset", 1.0f, _ball.GetComponent<BallScript>().Restart );	
		
		bool running = true;

        while (running) { // gameloop
            Application.DoEvents(); // empty forms event queue
            // can close
            if (Input.Key.Enter(Keys.Escape)) {
                running = false;
            }
            _window.Refresh(); // use refresh for a frame based update, async
        }
	}

	public void Update(Graphics graphics) {
		Time.Update();
		FrameCounter.Update();

        currentUpdateTime = Time.Now;
        lag += currentUpdateTime - previousUpdateTime;
        previousUpdateTime = currentUpdateTime;
        int counter = 0;
        //Console.WriteLine("Lag: " + lag);
        while (lag >= MS_PER_UPDATE) {
            Time.UpdateUpdate(); //updates the timestep based on actual gamplay updates instead of on frame updates;
            switch (_gameState) {
                case GameState.GameStart:
                    if (Input.Key.Enter(Keys.Enter)) {
                        SetGameState(GameState.Running);
                    }
                    break;
                case GameState.Running:
                    updateGameObjects();
                    _collisionManager.Update();
                    break;
                case GameState.Paused:
                    updateGameObjects();
                    break;
                case GameState.GameOver:
                    if (Input.Key.Enter(Keys.R)) SetGameState(GameState.GameStart);
                    break;
                default:
                    break;
            }
            
            Locator.EventManager.DeliverEvents(); 
            lag -= MS_PER_UPDATE;
            counter++;
        }
        //Console.WriteLine("Updates this frame: " + counter);
        
        drawGameObjects(graphics, (float)(lag / MS_PER_UPDATE));
    }
	
	public void Close() {
		_window.Close();
	}
	
	static public Random Random {
		get {
			return random;
		}
	}

    void PauseHandler(object sender, PauseEvent e) {
        _gameState = e.state == PauseState.Paused ? GameState.Paused : GameState.Running;
    }

    //Gets called at GameObject's construction time.
    public void AddToGameObjects(GameObject gameObject) {
        Debug.Assert(gameObject != null);
        gameObjectList.Add(gameObject);
    }

    public void AddToDrawables(RenderComponent drawable) {
        Debug.Assert(drawable != null);
        drawableList.Add(drawable);
    }

    private void printGameObjectList() {
        foreach (GameObject go in gameObjectList) {
            Console.WriteLine(go.Name);
        }
    }

    private void updateGameObjects() {
        for (int i = gameObjectList.Count - 1; i >= 0; i--) {
            gameObjectList[i].Update();
            //Console.WriteLine("{0} \t\t Got Updated!", gameObjectList[i].Name);
        }
    }

    private void drawGameObjects(Graphics graphics, float timeIntoNextFrame) {
        //Console.WriteLine("Drawables: " + drawableList.Count);
        if (_gameState != GameState.Running) timeIntoNextFrame = 0.0f;
        for (int i = drawableList.Count - 1; i >= 0; i--) {

            drawableList[i].Draw(graphics, timeIntoNextFrame);
        }
    }

    public GameObject FindGameObject(string pName) {
        return gameObjectList.Find(go => go.Name == pName);
    }

    public void AddComponentToStartQue(Component component) {
        componentStartQue[_startQueTail] = component;
        _startQueTail = (_startQueTail + 1) % MAX_STARTEVENTS;
    }

    private void doComponentEvents() {
        while (_startQueTail != _startQueHead) {
            componentStartQue[_startQueHead].Start();
            _startQueHead = (_startQueHead + 1) % MAX_STARTEVENTS;
        }
    }

    public void SetGameState(GameState gameState) {
        if (gameState == GameState.GameStart) {
            Locator.EventManager.AddEvent(new RestartEvent());
        }

        _gameState = gameState;
    }

    
}


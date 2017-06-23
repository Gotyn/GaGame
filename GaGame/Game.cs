using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

public enum GameState { GameStart, Running, Paused, GameOver }

public class Game {
    static private Random _random = new Random(0); // seed for repeatability.
    private Window _window;
    private GameState _gameState;
    private CollisionManager _collisionManager;

    static public Random Random { get => _random; }

    private GameObject _ball;
    GameObject _leftPaddle, _rightPaddle;
    private RenderComponent _infoSpriteRenderer;

    #region Lists Variables
    private List<GameObject> gameObjectList = new List<GameObject>();
    private List<RenderComponent> drawableList = new List<RenderComponent>();
    #endregion

    #region GameLoop Variables
    static double MS_PER_UPDATE = 0.016;  //Time 
    double currentUpdateTime;   //Time @ update start
    double previousUpdateTime;  //Time @ previous update start
    double lag = 0.0;
    #endregion

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

    private void Build() {
        #region Managers
        GameObject _gameManager = new GameObject("GameManager");
        _gameManager.AddComponent<GameManager>();
        #endregion

        #region Bounds
        GameObject _upperBound = new GameObject("UpperBound", new Vec2(0,-10));
        _upperBound.AddComponent<RenderComponent>().Image = Image.FromFile("width.png");
        _upperBound.AddComponent<Collider>();

        GameObject _lowerBound = new GameObject("LowerBound", new Vec2(0, 480));
        _lowerBound.AddComponent<RenderComponent>().Image = Image.FromFile("width.png");
        _lowerBound.AddComponent<Collider>();

        GameObject _leftBound = new GameObject("LeftBound", new Vec2(-10, 0));
        _leftBound.AddComponent<RenderComponent>().Image = Image.FromFile("height.png");
        _leftBound.AddComponent<Collider>();

        GameObject _rightBound = new GameObject("RightBound", new Vec2(640, 0));
        _rightBound.AddComponent<RenderComponent>().Image = Image.FromFile("height.png");
        _rightBound.AddComponent<Collider>();
        #endregion 

        #region Info Sprite
        GameObject _infoSprite = new GameObject("GameOverSprite", new Vec2(170, 190));
        _infoSpriteRenderer = _infoSprite.AddComponent<RenderComponent>();
        _infoSpriteRenderer.Image = Image.FromFile("start.png");
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
        _leftPaddle.AddComponent<AutoPaddleScript>();

        _rightPaddle = new GameObject("RightPaddle", new Vec2(622, 208));
        _rightPaddle.AddComponent<RigidBody>();
        _rightPaddle.AddComponent<Collider>();
        _rightPaddle.AddComponent<RenderComponent>().Image = Image.FromFile("paddle.png");
        _rightPaddle.AddComponent<PaddleScript>();
        #endregion

        #region Scores
        GameObject _leftScore = new GameObject("LeftScore", new Vec2(320 - 20 - 66, 10));
        _leftScore.AddComponent<TextComponent>().Paddle = _leftPaddle;
        _leftScore.GetComponent<TextComponent>().Image = Image.FromFile("digits.png");

        GameObject _rightScore = new GameObject("RightScore", new Vec2(320 + 20, 10));
        _rightScore.AddComponent<TextComponent>().Paddle = _rightPaddle;
        _rightScore.GetComponent<TextComponent>().Image = Image.FromFile("digits.png");
        #endregion

        #region Boosters
        GameObject _booster1 = new GameObject("Booster1", new Vec2(304, 96));
        _booster1.AddComponent<RenderComponent>().Image = Image.FromFile("booster.png");
        _booster1.AddComponent<Collider>();
        _booster1.AddComponent<BoosterScript>();

        GameObject _booster2 = new GameObject("Booster2", new Vec2(304, 384));
        _booster2.AddComponent<RenderComponent>().Image = Image.FromFile("booster.png");
        _booster2.AddComponent<Collider>();
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
        while (lag >= MS_PER_UPDATE) {
            Time.UpdateBasedOnUpdate(); //updates the timestep based on actual gamplay updates instead of on frame updates;
            switch (_gameState) {
                case GameState.GameStart:
                    if (Input.Key.Enter(Keys.D1)) {
                        SetGameState(GameState.Running);
                    }
                    if (Input.Key.Enter(Keys.D2)) {
                        _leftPaddle.RemoveComponentsOfType<AutoPaddleScript>();
                        _leftPaddle.AddComponent(new PaddleScript(Keys.W, Keys.S));
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
        drawGameObjects(graphics, (float)(lag / MS_PER_UPDATE));
    }
	
	public void Close() {
		_window.Close();
	}
	
    void PauseHandler(object sender, PauseEvent e) {
        _gameState = e.state == PauseState.Paused ? SetGameState(GameState.Paused) : SetGameState(GameState.Running);
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

    public void RemoveFromDrawables(RenderComponent drawable) {
        Debug.Assert(drawable != null);
        drawableList.Remove(drawable);
    }

    private void updateGameObjects() {
        for (int i = gameObjectList.Count - 1; i >= 0; i--) {
            gameObjectList[i].Update();
        }
    }

    private void drawGameObjects(Graphics graphics, float timeIntoNextFrame) {
        if (_gameState != GameState.Running) timeIntoNextFrame = 0.0f;
        for (int i = drawableList.Count - 1; i >= 0; i--) {
            drawableList[i].Draw(graphics, timeIntoNextFrame);
        }
    }

    public GameObject FindGameObject(string pName) {
        return gameObjectList.Find(go => go.Name == pName);
    }

    public GameState SetGameState(GameState gameState) {
        switch (gameState) {
            case GameState.GameStart:
                _infoSpriteRenderer.Image = Image.FromFile("start.png");
                _infoSpriteRenderer.Enabled = true;
                Locator.EventManager.AddEvent(new RestartEvent());
                break;
            case GameState.Running:
                _infoSpriteRenderer.Enabled = false;
                break;
            case GameState.Paused:
                Console.WriteLine("Happened");
                _infoSpriteRenderer.Image = Image.FromFile("pause.png");
                _infoSpriteRenderer.Enabled = true;
                break;
            case GameState.GameOver:
                _infoSpriteRenderer.Image = Image.FromFile("gameover.png");
                _infoSpriteRenderer.Enabled = true;
                break;
            default:
                _infoSpriteRenderer.Enabled = false;
                break;
        }
        _gameState = gameState;
        return gameState;
    }
}


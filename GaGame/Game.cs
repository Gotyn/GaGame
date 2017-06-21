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

public class Game {
    static private Random random = new Random(0); // seed for repeatability.
    private Window _window;

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
	}

    private CollisionManager _collisionManager;
    
    //GameObjects
    private GameObject ball;
    private GameObject leftPaddle, rightPaddle;
    private GameObject leftScore, rightScore;
    private GameObject booster1, booster2;

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
        #region CollisionManager
        _collisionManager = new CollisionManager(this);
        #endregion

        #region Ball 
        ball = new GameObject(this, "Ball");
        ball.AddComponent<RigidBody>();
        ball.AddComponent<Collider>();
        ball.AddComponent<RenderComponent>().Image = Image.FromFile("ball.png");
        ball.AddComponent<BallScript>();
        #endregion

        #region Paddles
        leftPaddle = new GameObject(this, "LeftPaddle", new Vec2(10, 208));
        leftPaddle.AddComponent<RigidBody>();
        leftPaddle.AddComponent<Collider>();
        leftPaddle.AddComponent<RenderComponent>().Image = Image.FromFile("paddle.png");
        leftPaddle.AddComponent<PaddleInput>();
        leftPaddle.AddComponent<PaddleScript>();

        rightPaddle = new GameObject(this, "RightPaddle", new Vec2(622, 328 /*622, 208*/));
        rightPaddle.AddComponent<RigidBody>();
        rightPaddle.AddComponent<Collider>();
        rightPaddle.AddComponent<RenderComponent>().Image = Image.FromFile("paddle.png");
        rightPaddle.AddComponent<PaddleInput>();
        rightPaddle.AddComponent<PaddleScript>();
        #endregion

        #region Scores
        //LeftScore
        leftScore = new GameObject(this, "LeftScore", new Vec2(320 - 20 - 66, 10));
        leftScore.AddComponent<TextComponent>().Paddle = leftPaddle;
        leftScore.GetComponent<TextComponent>().Image = Image.FromFile("digits.png");

        //RightScore
        rightScore = new GameObject(this, "RightScore", new Vec2(320 + 20, 10));
        rightScore.AddComponent<TextComponent>().Paddle = rightPaddle;
        rightScore.GetComponent<TextComponent>().Image = Image.FromFile("digits.png");
        #endregion

        #region Boosters
        //Booster1
        booster1 = new GameObject(this, "Booster1", new Vec2(304, 96));
        booster1.AddComponent<RenderComponent>().Image = Image.FromFile("booster.png");
        booster1.AddComponent<BoosterScript>();
        
        //Booster2
        booster2 = new GameObject(this, "Booster2", new Vec2(304, 384));
        booster2.AddComponent<RenderComponent>().Image = Image.FromFile("booster.png");
        booster2.AddComponent<BoosterScript>();
        #endregion

        HandleEvents();
    }

    public void Run() {
		Time.Timeout( "Reset", 1.0f, ball.GetComponent<BallScript>().Restart );	
		
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
            updateGameObjects();
            _collisionManager.Update();
            HandleEvents();

            //ScoreBounds
            if (ball.Position.X < 0) {
                rightPaddle.GetComponent<PaddleScript>().IncScore();
                ball.GetComponent<BallScript>().Reset();
            }
            if (ball.Position.X > 640 - 16) { // note: bad literals detected
                leftPaddle.GetComponent<PaddleScript>().IncScore();
                ball.GetComponent<BallScript>().Reset();
            }

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
        for (int i = drawableList.Count - 1; i >= 0; i--) {

            drawableList[i].Draw(graphics, timeIntoNextFrame);
        }
    }

    public GameObject FindGameObject(string pName) {
        return gameObjectList.Find(go => go.Name == pName);
    }

    public void HandleEvents() {
        _collisionManager.DoCollisionEvents();
        doComponentEvents();
    }

    public void RegisterForCollisionChecks(GameObject gameObject) {
        _collisionManager.AddCollidingObject(gameObject);
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

}


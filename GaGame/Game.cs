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
    
    private GameObject ball;
    private GameObject leftPaddle, rightPaddle;
    private GameObject leftScore, rightScore;
    private GameObject booster1, booster2;

    GameObject dummy;

    private List<GameObject> gameObjectList = new List<GameObject>();
    private List<GameObject> drawableList = new List<GameObject>();

    private void Build() {
        _collisionManager = new CollisionManager(this);

        //Ball
        ball = new GameObject(this, "Ball");
        ball.AddComponent<RigidBody>();
        ball.AddComponent<RenderComponent>().Image = Image.FromFile("ball.png");
        ball.AddComponent<PhysicsComponent>();
        ball.AddComponent<BallScript>();

        //LeftPaddle
        leftPaddle = new GameObject(this, "LeftPaddle", new Vec2(10, 208));
        leftPaddle.AddComponent<RigidBody>();
        leftPaddle.AddComponent<RenderComponent>().Image = Image.FromFile("paddle.png");
        leftPaddle.AddComponent<PhysicsComponent>();
        leftPaddle.AddComponent<PaddleInput>();
        leftPaddle.AddComponent<PaddleScript>();

        ////dummypaddleforcollisioncheck
        //dummy = new GameObject(this, "Dummy", new Vec2(3, 100));
        //dummy.AddComponent<RenderComponent>().Image = Image.FromFile("paddle.png");

        //RightPaddle
        rightPaddle = new GameObject(this, "RightPaddle", new Vec2(622, 328 /*622, 208*/));
        rightPaddle.AddComponent<RigidBody>();
        rightPaddle.AddComponent<RenderComponent>().Image = Image.FromFile("paddle.png");
        rightPaddle.AddComponent<PhysicsComponent>();
        rightPaddle.AddComponent<PaddleInput>();
        rightPaddle.AddComponent<PaddleScript>();

        //LeftScore
        leftScore = new GameObject(this, "LeftScore", new Vec2(320 - 52 - 66, 10));
        leftScore.AddComponent<RenderComponent>().Image = Image.FromFile("digits.png");
        leftScore.AddComponent<TextScript>().Paddle = leftPaddle;

        //RightScore
        rightScore = new GameObject(this, "RightScore", new Vec2(320 + 20, 10));
        rightScore.AddComponent<RenderComponent>().Image = Image.FromFile("digits.png");
        rightScore.AddComponent<TextScript>().Paddle = rightPaddle;


        //Booster1
        booster1 = new GameObject(this, "Booster1", new Vec2(304, 96));
        booster1.AddComponent<RenderComponent>().Image = Image.FromFile("booster.png");

        //Booster2
        booster2 = new GameObject(this, "Booster2", new Vec2(304, 384));
        booster2.AddComponent<RenderComponent>().Image = Image.FromFile("booster.png");

        //printGameObjectList();
    }

    public void Run() {
		Time.Timeout( "Reset", 1.0f, ball.GetComponent<BallScript>().Restart );	
		
		bool running = true;
		while( running ) { // gameloop
			Application.DoEvents(); // empty forms event queue

			// can close
			if( Input.Key.Enter( Keys.Escape ) ) {
				running = false;
			}
			
			_window.Refresh(); // use refresh for a frame based update, async
		}
	}

	public void Update(Graphics graphics) {
		Time.Update();
		FrameCounter.Update();

        // steps to do
        // input
        // apply velocity so move		
        // check collisions and apply reponse and rules		
        // render


        UpdateGameObjects();
        _collisionManager.CheckCollision(ball, rightPaddle);
        _collisionManager.CheckCollision(ball, leftPaddle);
        DrawDrawables(graphics);

        //if (ball.Position.X < 0) {
        //    rightPaddle.GetComponent<PaddleScript>().IncScore();
        //    ball.GetComponent<BallScript>().Reset();
        //}
        //if (ball.Position.X > 640 - 16) { // note: bad literals detected
        //    leftPaddle.GetComponent<PaddleScript>().IncScore();
        //    ball.GetComponent<BallScript>().Reset();
        //}

        Thread.Sleep( 16 ); // roughly 60 fps
		
		//Console.WriteLine("Updating");
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

    public void AddToDrawables(GameObject gameObject) {
        Debug.Assert(gameObject != null);
        drawableList.Add(gameObject);
    }

    private void printGameObjectList() {
        foreach (GameObject go in gameObjectList) {
            Console.WriteLine(go.Name);
        }
    }

    private void printDrawables() {
        foreach (GameObject go in drawableList) {
            Console.WriteLine(go.Name);
        }
    }

    private void UpdateGameObjects() {
        for (int i = gameObjectList.Count - 1; i >= 0; i--) {
            gameObjectList[i].Update();
            //Console.WriteLine("{0} \t\t Got Updated!", gameObjectList[i].Name);
        }
    }

    private void DrawDrawables(Graphics graphics) {
        //Console.WriteLine("Drawables: " + drawableList.Count);
        for (int i = drawableList.Count - 1; i >= 0; i--) {

            drawableList[i].GetComponent<RenderComponent>().Draw(graphics);
        }
    }

    public GameObject FindGameObject(string pName) {
        return gameObjectList.Find(go => go.Name == pName);
    }
}


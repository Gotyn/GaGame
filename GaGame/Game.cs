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
    private Graphics _graphics;

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


    //GameObjects
    public Ball ball;
    private Text leftScore, rightScore;
    public Paddle leftPaddle, rightPaddle;
    private Booster booster1, booster2;

    private List<GameObject> gameObjectList = new List<GameObject>();
    private List<GameObject> drawableList = new List<GameObject>();

    private void Build() {
        ball = new Ball(this, "Ball", "ball.png"); // orbitting the window centre

        leftPaddle = new AutoPaddle(this, "Left", 10, 208, "paddle.png", ball);
        rightPaddle = new AutoPaddle(this, "Right", 622, 208, "paddle.png", ball);

        //leftPaddle = new Paddle("Left", 10, 208, "paddle.png", ball);
        //rightPaddle = new Paddle("Right", 622, 208, "paddle.png", ball);

        //leftPaddle = new CurvedPaddle("Left", 10, 208, "paddle.png", ball);
        //rightPaddle = new CurvedPaddle("Right", 622, 208, "paddle.png", ball);

        leftScore = new Text(this, "LeftScore", 320 - 20 - 66, 10, "digits.png", leftPaddle);
        rightScore = new Text(this, "RightScore", 320 + 20, 10, "digits.png", rightPaddle);
        booster1 = new Booster(this, "Booster", 304, 96, "booster.png", ball);
        booster2 = new Booster(this, "Booster", 304, 384, "booster.png", ball);

        //printGameObjectList();
    }

    //Gets called at GameObject's construction time.
    public void AddToGameObjects(GameObject gameObject) {
        Debug.Assert(gameObject != null);
        gameObjectList.Add(gameObject);
    }

    public void AddToDrawables(GameObject gameObject) {
        Debug.Assert(gameObject != null);
        gameObjectList.Add(gameObject);
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
        for (int i = gameObjectList.Count - 1; i >= 0; i-- ) {
            gameObjectList[i].Update();
            //Console.WriteLine("{0} \t\t Got Updated!", gameObjectList[i].Name);
        }
    }

    public GameObject Find(string pName) {
        return gameObjectList.Find(go => go.Name == pName);
    }

    public void Run() {
		Time.Timeout( "Reset", 1.0f, ball.Restart );	
		
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

	public void Update() {
		Time.Update();
		FrameCounter.Update();

        // steps to do
        // input
        // apply velocity so move		
        // check collisions and apply reponse and rules		
        // render

        UpdateGameObjects();

        if (ball.Position.X < 0) {
            rightPaddle.IncScore();
            ball.Reset();
        }
        if (ball.Position.X > 640 - 16) { // note: bad literals detected
            leftPaddle.IncScore();
            ball.Reset();
        }

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

    public Graphics Graphics { get => _graphics; set => _graphics = value; }
}


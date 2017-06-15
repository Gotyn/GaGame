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


    
    private GameObject ball;
    private BallScript ballScript;

    //private GameObject leftPaddle, rightPaddle;

    //private GameObject leftScore, rightScore;
    //private GameObject booster1, booster2;

    private List<GameObject> gameObjectList = new List<GameObject>();
    private List<GameObject> drawableList = new List<GameObject>();

    private void Build() {
        ball = new GameObject(this, "Ball");
        ballScript = new BallScript(ball);
        ball.AddComponent(ballScript);

        //leftPaddle = new AutoPaddle(this, "Left", 10, 208, "paddle.png", ball);
        //rightPaddle = new AutoPaddle(this, "Right", 622, 208, "paddle.png", ball);

        //leftPaddle = new Paddle("Left", 10, 208, "paddle.png", ball);
        //rightPaddle = new Paddle("Right", 622, 208, "paddle.png", ball);

        //leftPaddle = new ManualPaddleScript(this, "ManualPaddleLeft", 10, 208, "paddle.png", ball);
        //rightPaddle = new ManualPaddleScript(this, "ManualPaddleRight", 622, 208, "paddle.png", ball);

        //leftScore = new TextScript(this, "LeftScore", 320 - 20 - 66, 10, "digits.png", leftPaddle);
        //rightScore = new TextScript(this, "RightScore", 320 + 20, 10, "digits.png", rightPaddle);
        //booster1 = new BoosterScript(this, "Booster", 304, 96, "booster.png", ball);
        //booster2 = new BoosterScript(this, "Booster", 304, 384, "booster.png", ball);

        //printGameObjectList();
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

    public GameObject Find(string pName) {
        return gameObjectList.Find(go => go.Name == pName);
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
 }


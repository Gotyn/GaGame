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

public class Game
{
	[STAThread] // needed to use wpf Keyboard.isKeyPressed when single threaded !
	static
	public void Main() {
		Console.WriteLine( "Starting Game, close with Escape");
		Game game;
			game = new Game();
				game.Build();
				game.Run();
			game.Close();
		Console.WriteLine( "Closed window");

	}



	static private Random random = new Random( 0 ); // seed for repeatability.
	
	private Window window;
	
	private Text leftScore;
	private Text rightScore;
	
	private Ball ball;
	private Paddle leftPaddle;
	private Paddle rightPaddle;
	private Booster booster1;
	private Booster booster2;
	
	public Game()
	{
		window = new Window( this );
	}	

	private void Build() 
	{
		
		ball = new Ball( "Ball", "ball.png" ); // orbitting the window centre
		leftPaddle = new AutoPaddle( "Left", 10, 208, "paddle.png", ball );
		rightPaddle = new AutoPaddle( "Right", 622, 208, "paddle.png", ball );
		
		leftScore = new Text( "LeftScore", 320-20 - 66, 10, "digits.png", leftPaddle );
		rightScore = new Text( "RightScore", 320+20, 10, "digits.png", rightPaddle );

		booster1 = new Booster( "Booster", 304, 96, "booster.png", ball );
		booster2 = new Booster( "Booster", 304, 384, "booster.png", ball );
		
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
			
			window.Refresh(); // use refresh for a frame based update, async

		}
	}

	public void Update( Graphics pGraphics )
	{
		Time.Update();
		FrameCounter.Update();
		
		// steps to do
			// input
			// apply velocity so move		
			// check collisions and apply reponse and rules		
			// render

		ball.Update( pGraphics );
		leftPaddle.Update( pGraphics );
		rightPaddle.Update( pGraphics );
		booster1.Update( pGraphics );
		booster2.Update( pGraphics );
		
		leftScore.Update( pGraphics );
		rightScore.Update( pGraphics );
		
		if( ball.Position.X < 0 ) {
			rightPaddle.IncScore();
			ball.Reset();
		}		
		if( ball.Position.X > 640-16 ) { // note: bad literals detected
			leftPaddle.IncScore();
			ball.Reset();
		}
		
		Thread.Sleep( 16 ); // roughly 60 fps
		
		//Console.WriteLine("Updating");
	}
	
	public void Close() {
		window.Close();
	}
	
	static public Random Random {
		get {
			return random;
		}
	}

}


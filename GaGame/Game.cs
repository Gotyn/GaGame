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
    private World world;
	
	public Game()
	{
		window = new Window( this );
	}	

	private void Build() 
	{
        world = new World();
    }

    public void Run() {
		Time.Timeout( "Reset", 1.0f, world.ball.Restart );	
		
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

        world.Update( pGraphics );
        		
		Thread.Sleep( 16 ); // roughly 60 fps
		
		//Console.WriteLine("Updating");
	}

    public void Draw( Graphics graphics ) {
        //graphics.DrawImage(image, position.X, position.Y);
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


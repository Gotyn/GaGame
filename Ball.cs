/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class Ball
{
	private string name;
	private Image image;

	private Vec2 position = null; // making clear no default value, needs constructor action.
	private Vec2 velocity = null;
	
	private bool pausing = true;
	
	public readonly Vec2 Speed = new Vec2( 10.0f, 10.0f );
	
	public Ball( string pName, string pImageFile )
	{
		name = pName;
		image = Image.FromFile( pImageFile );
		position = new Vec2( 312, 232 ); // center of form
		velocity = new Vec2( 0.0f, 0.0f );
		Reset(); // sets pos and vel
	}
	
	public void Update( Graphics graphics )
	{
		// input
		if( Input.Key.Enter( Keys.P ) ) {
			pausing = ! pausing; // toggle
			Console.WriteLine( "Pausing "+pausing );
		}
		
		// move
		if( ! pausing ) {
			position.Add( velocity );
		}
		
		// collisions & resolve

		// Y bounds reflect
		if( position.Y < 0 ) { 
			position.Y = 0;
			velocity.Y = -velocity.Y;
		}
		if( position.Y > 480-16 ) { // note: non maintainable literals here, who did this
			position.Y = 480-16;
			velocity.Y = -velocity.Y;
		}
		
		// see game and paddles
		
		// graphics
		graphics.DrawImage( image, position.X, position.Y );
	}	

	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
		    this.position.X < otherPosition.X+otherSize.X && this.position.X + this.Size.X > otherPosition.X &&
		    this.position.Y < otherPosition.Y+otherSize.Y && this.position.Y + this.Size.Y > otherPosition.Y;
	}
	
	
	
	public void Boost() {
		velocity = velocity * 2.0f;
	}

	public void DeBoost() {
		velocity = velocity / 2.0f;
	}
	
	
	public void Reset() 
	{
		position.X = 320-8;
		position.Y = 240-8;
		//velocity.X = 0.5f;
		velocity.X = Speed.X;
		velocity.Y = (float)(Game.Random.NextDouble() - 0.5) * 2.0f * Speed.Y;
		pausing = true;
		Time.Timeout( "Reset", 1.0f, Restart );	// restart after 1 sec.
	}
	 
	public Vec2 Center {
		get {
			return position + 0.5f * Size;
		}
	}	
	public Vec2 Position {
		get { 
			return position;
		}
	}
	public Vec2 Size {
		get { 
			return new Vec2( image.Width, image.Height ); 
		}
	}
	public Vec2 Velocity {
		get {
			return velocity;
		}
	}
	
	public void Restart(  Object sender,  Time.TimeoutEvent timeout ) 
	{
		pausing = false;
		Console.WriteLine("Restart");
	}

}


/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class Paddle
{
	protected string name;
	protected Image image;

	protected Vec2 position = null; // making clear no default value, needs constructor action.
	protected Vec2 velocity = null;
	
	protected Ball ball = null;
	protected uint score;
	
	public const float Speed = 5.0f;
	
	
	public Paddle( string pName, float pX, float pY, string pImageFile, Ball pBall )
	{
		name = pName;
		image = Image.FromFile( pImageFile );
		position = new Vec2( pX, pY );
		velocity = new Vec2( 0, 0 );
		ball = pBall;
		score = 0;		
	}
	
	virtual public void Update( Graphics graphics )
	{
		// input
		
		velocity.Y = 0; // no move 
		if ( Input.Key.Pressed( Keys.Up ) ) velocity.Y = -Speed;
		if ( Input.Key.Pressed( Keys.Down ) ) velocity.Y = Speed;
		
		// move
		position.Add( velocity );
		
		// collisions & resolve
		if( Intersects( ball.Position, ball.Size ) ) {
			if( ball.Velocity.X > 0 ) {
				ball.Position.X = position.X - ball.Size.X;
			} else if( ball.Velocity.X < 0 ) {
				ball.Position.X = position.X + Size.X;
			}
			ball.Velocity.X = -ball.Velocity.X;
		}
		
		// collisions
		if( position.Y < 0 ) position.Y = 0;
		if( position.Y > 416 ) position.Y = 416;
		
		// render
		graphics.DrawImage( image, position.X, position.Y );
	}	
	
	public void IncScore() 
	{
		score++;
	}

	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
		    this.position.X < otherPosition.X+otherSize.X && this.position.X + this.Size.X > otherPosition.X &&
		    this.position.Y < otherPosition.Y+otherSize.Y && this.position.Y + this.Size.Y > otherPosition.Y;
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

	public uint Score {
		get { 
			return score; 
		}
	}	
	

}



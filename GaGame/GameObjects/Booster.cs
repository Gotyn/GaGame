/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class Booster : GameObject
{
	private bool active = true;
	private Ball ball;
	
	public Booster( string pName, float pX, float pY, string pImageFile, Ball pBall ) : base (pName)
	{
		image = Image.FromFile( pImageFile );
		position = new Vec2( pX, pY );
		
		ball = pBall;		
	}

    override public void Update() {
        // input

        // move

        // collisions & resolve
        //Console.WriteLine( active );
        if (active && Intersects(ball.Position, ball.Size)) {
            active = false;
            ball.Boost();
            Time.Timeout("Deboosting", 0.5f, DeBoost);
        }
    }

    public void Update( Graphics graphics )
	{
        // Render
		graphics.DrawImage( image, position.X, position.Y );
	}	
	
	
	// Event handlers

	private void DeBoost( Object sender,  Time.TimeoutEvent timeout )
	{
		ball.DeBoost();
		active = true;
		//Console.WriteLine( "Deboosting "+name );
	}	

	// Tools
	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
		    this.position.X < otherPosition.X+otherSize.X && this.position.X + this.Size.X > otherPosition.X &&
		    this.position.Y < otherPosition.Y+otherSize.Y && this.position.Y + this.Size.Y > otherPosition.Y;
	}
	
	
	public void Reset() 
	{
		position.X = 320-8;
		position.Y = 240-8;
	}
	
}


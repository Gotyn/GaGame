/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class BoosterScript : Component
{
	public bool active = true;
	public BallScript ball;
	
	public BoosterScript(Game pGame, string pName, float pX, float pY, string pImageFile, GameObject pBall) {
        Owner.image = Image.FromFile( pImageFile );
        Owner.position = new Vec2( pX, pY );
		
		ball = pBall.GetComponent<BallScript>();

        Owner.AddComponent(new tempBoosterComp());
        Owner.AddComponent(new RenderComponent());
	}

	// Event handlers

	public void DeBoost( Object sender,  Time.TimeoutEvent timeout )
	{
		ball.DeBoost();
		active = true;
		//Console.WriteLine( "Deboosting "+name );
	}	

	// Tools
	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
            Owner.position.X < otherPosition.X+otherSize.X && Owner.position.X + Owner.Size.X > otherPosition.X &&
            Owner.position.Y < otherPosition.Y+otherSize.Y && Owner.position.Y + Owner.Size.Y > otherPosition.Y;
	}
	
	
	public void Reset() 
	{
        Owner.position.X = 320-8;
        Owner.position.Y = 240-8;
	}
	
}


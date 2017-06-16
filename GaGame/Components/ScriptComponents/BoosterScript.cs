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
        Owner.Position = new Vec2( pX, pY );
		
		ball = pBall.GetComponent<BallScript>();
    }

    // Event handlers
    public override void Update() {
        //if (_booster.active && _booster.Intersects(_booster.ball.Owner.Position, _booster.ball.Owner.Size)) {
        //    _booster.active = false;
        //    _booster.ball.Boost();
        //    Time.Timeout("Deboosting", 0.5f, _booster.DeBoost);
        //}
    }


    public void DeBoost( Object sender,  Time.TimeoutEvent timeout )
	{
		ball.DeBoost();
		active = true;
		//Console.WriteLine( "Deboosting "+name );
	}	

	// Tools
	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
        return
            false; //Owner.position.X < otherPosition.X+otherSize.X && Owner.position.X + Owner.Size.X > otherPosition.X &&
                  //Owner.position.Y < otherPosition.Y+otherSize.Y && Owner.position.Y + Owner.Size.Y > otherPosition.Y;
    }
	
	
	public void Reset() 
	{
        Owner.Position.X = 320-8;
        Owner.Position.Y = 240-8;
	}
	
}


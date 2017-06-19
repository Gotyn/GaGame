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
	
    public override void Start() {
        ball = Owner.Game.FindGameObject("Ball").GetComponent<BallScript>();
    }

    // Event handlers
    public override void Update() {
        if (active && Intersects(ball.Owner.Position, ball.Owner.GetComponent<RenderComponent>().Size)) {
            active = false;
            ball.Boost();
            Time.Timeout("Deboosting", 0.5f, DeBoost);
        }
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
            Owner.Position.X < otherPosition.X+otherSize.X && Owner.Position.X + Owner.Size.X > otherPosition.X &&
            Owner.Position.Y < otherPosition.Y+otherSize.Y && Owner.Position.Y + Owner.Size.Y > otherPosition.Y;
    }
	
	
	public void Reset() 
	{
        Owner.Position.X = 320-8;
        Owner.Position.Y = 240-8;
	}
	
}


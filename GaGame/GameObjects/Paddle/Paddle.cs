/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class Paddle : GameObject
{
	//protected Vec2 velocity = null;
	
	public Ball ball = null;
	protected uint score;
	
	public const float Speed = 5.0f;

    public RigidBody _rigidBody;
	
	public Paddle(Game pGame, string pName, float pX, float pY, string pImageFile, Ball pBall) : base(pGame, pName) {
		image = Image.FromFile( pImageFile );
		position = new Vec2( pX, pY );
		//velocity = new Vec2( 0, 0 );
		ball = pBall;
		score = 0;

        AddComponent(new RigidBody());
        //AddComponent(new PaddleInput());

        //AddComponent(new tempAutoPaddleComp());
        //AddComponent(new tempManualPaddleComp());
        
        //AddComponent(new PhysicsComponent());
        AddComponent(new RenderComponent());

        Console.WriteLine("Happens___0");
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
	
	public uint Score {
		get { 
			return score; 
		}
	}	
	

}



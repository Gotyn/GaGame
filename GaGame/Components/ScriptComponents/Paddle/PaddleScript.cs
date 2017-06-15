/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


abstract public class PaddleScript : Component
{
	//protected Vec2 velocity = null;
	
	public BallScript ball = null;
	protected uint score;
	
	public const float Speed = 5.0f;

    public RigidBody _rigidBody;
	
	public PaddleScript(Game pGame, string pName, float pX, float pY, string pImageFile, GameObject pBall) { 
        Owner.image = Image.FromFile( pImageFile );
        Owner.position = new Vec2( pX, pY );
        //velocity = new Vec2( 0, 0 );
        ball = pBall.GetComponent<BallScript>();
		score = 0;

        Owner.AddComponent(new RigidBody());
        Owner.AddComponent(new PhysicsComponent());
        Owner.AddComponent(new RenderComponent());
    }

	public void IncScore() 
	{
		score++;
	}

	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
            Owner.position.X < otherPosition.X+otherSize.X && Owner.position.X + Owner.Size.X > otherPosition.X &&
            Owner.position.Y < otherPosition.Y+otherSize.Y && Owner.position.Y + Owner.Size.Y > otherPosition.Y;
	}
	
	public uint Score {
		get { 
			return score; 
		}
	}	
	

}



/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class Ball : GameObject
{
    //private Vec2 velocity = null;
	
	public bool pausing = true;
	
	public readonly Vec2 Speed = new Vec2( 10.0f, 10.0f );

    public RigidBody _rigidBody;


    public Ball( string pName, string pImageFile, GameObject pParent = null ) : base (pName, pParent)
	{
		image = Image.FromFile( pImageFile );
		position = new Vec2( 312, 232 ); // center of form

        AddComponent(new RenderComponent());
        AddComponent(new PhysicsComponent());

        _rigidBody = new RigidBody();
        AddComponent(_rigidBody);
        AddComponent(new tempBallComp());

        Reset(); // sets pos and vel
    }

	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
		    this.position.X < otherPosition.X+otherSize.X && this.position.X + this.Size.X > otherPosition.X &&
		    this.position.Y < otherPosition.Y+otherSize.Y && this.position.Y + this.Size.Y > otherPosition.Y;
	}
	
	
	public void Boost() {
		_rigidBody.Velocity = _rigidBody.Velocity * 2.0f;
	}

	public void DeBoost() {
		_rigidBody.Velocity = _rigidBody.Velocity / 2.0f;
	}


    public void Reset() {
        position.X = 320 - 8;
        position.Y = 240 - 8;
        //velocity.X = 0.5f;
         _rigidBody.Velocity = new Vec2(Speed.X, (float)(Game.Random.NextDouble() - 0.5) * 2.0f * Speed.Y);
        pausing = true;
        Time.Timeout("Reset", 1.0f, Restart);   // restart after 1 sec.
    }

	public void Restart(  Object sender,  Time.TimeoutEvent timeout ) 
	{
		pausing = false;
		Console.WriteLine("Restart");
	}

}


/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;


public class BallScript : Component
{
    //private Vec2 velocity = null;
	
	public bool pausing = true;
	
	public readonly Vec2 Speed = new Vec2( 10.0f, 10.0f );

    public RigidBody _rigidBody;

    private GameObject _owner;


    public BallScript(GameObject owner) {
        _owner = owner;

        //owner.image = Image.FromFile(pImageFile);
        _owner.position = new Vec2(312, 232); // center of form

        _rigidBody = new RigidBody();
        _owner.AddComponent(_rigidBody);
        _owner.AddComponent(new tempBallComp());
        _owner.AddComponent(new RenderComponent());
        _owner.AddComponent(new PhysicsComponent());

        Reset(); // sets pos and vel
    }

    public override void Update() {
        
    }

    public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
            _owner.position.X < otherPosition.X+otherSize.X && _owner.position.X + _owner.Size.X > otherPosition.X &&
            _owner.position.Y < otherPosition.Y+otherSize.Y && _owner.position.Y + _owner.Size.Y > otherPosition.Y;
	}
	
	
	public void Boost() {
		_rigidBody.Velocity = _rigidBody.Velocity * 2.0f;
	}

	public void DeBoost() {
		_rigidBody.Velocity = _rigidBody.Velocity / 2.0f;
	}


    public void Reset() {
        _owner.position.X = 320 - 8;
        _owner.position.Y = 240 - 8;
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


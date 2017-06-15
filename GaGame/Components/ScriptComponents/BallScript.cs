/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;


public class BallScript : Component
{
	public readonly Vec2 Speed = new Vec2( 10.0f, 10.0f );
    private RigidBody _rigidBody;

    public override void Start() {
        Owner.Position = new Vec2(312, 232); // center of form

        _rigidBody = Owner.GetComponent<RigidBody>();
        Debug.Assert(_rigidBody != null);

        Reset(); // sets pos and vel
    }

    public override void Update() {
        // collisions & resolve
        
        // Y bounds reflect
        if (Owner.Position.Y < 0) {
            Owner.Position.Y = 0;
            _rigidBody.Velocity.Y = -_rigidBody.Velocity.Y;
        }
        if (Owner.Position.Y > 480 - 16) { // note: non maintainable literals here, who did this
            Owner.Position.Y = 480 - 16;
            _rigidBody.Velocity.Y = -_rigidBody.Velocity.Y;
        }
    }

    public void Boost() {
		_rigidBody.Velocity = _rigidBody.Velocity * 2.0f;
	}

	public void DeBoost() {
		_rigidBody.Velocity = _rigidBody.Velocity / 2.0f;
	}


    public void Reset() {
        Owner.Position.X = 320 - 8;
        Owner.Position.Y = 240 - 8;
        //velocity.X = 0.5f;
        Owner.GetComponent<RigidBody>().Velocity = new Vec2(Speed.X, (float)(Game.Random.NextDouble() - 0.5) * 2.0f * Speed.Y);
        Time.Timeout("Reset", 1.0f, Restart);   // restart after 1 sec.
    }

	public void Restart(  Object sender,  Time.TimeoutEvent timeout ) 
	{
		Console.WriteLine("Restart");
	}

}


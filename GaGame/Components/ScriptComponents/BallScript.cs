using System;
using System.Diagnostics;

public class BallScript : Component {
	public readonly Vec2 Speed = new Vec2( 5.0f, 5.0f );
    private RigidBody _rigidBody;

    public override void Start() {
        Owner.Position = new Vec2(312, 232); // center of form
        RestartEvent.Handlers += this.RestartHandler;
        _rigidBody = Owner.GetComponent<RigidBody>();
        Debug.Assert(_rigidBody != null, "_rigidBody in ballScript is NULL");
        Reset(); // sets pos and vel
    }

    public void Boost() {
		_rigidBody.Velocity *= 2.0f;
	}

	public void DeBoost() {
		_rigidBody.Velocity /= 2.0f;
	}

    public void Reset() {
        Owner.Position.X = 320 - 8;
        Owner.Position.Y = 240 - 8;
        _rigidBody.Velocity = new Vec2(0, 0);
        Time.Timeout("Reset", 1.0f, Restart);   // restart after 1 sec.
    }

	public void Restart(  Object sender,  Time.TimeoutEvent timeout ) {
        _rigidBody.Velocity = new Vec2(Speed.X * (Game.Random.Next(0,2) == 0 ? -1 : 1), (float)(Game.Random.NextDouble() - 0.5) * 2.0f * Speed.Y);  //randomize which way the ball will go from start
        Console.WriteLine("Restart");
	}

    void RestartHandler(object sender, RestartEvent e) {
        Reset();
    }

    override public void  OnCollision(GameObject other) {
        if (other.Name == "LeftPaddle" || other.Name == "RightPaddle") {
            _rigidBody.UndoMove();
            _rigidBody.Velocity.X *= -1;
        }
        if (other.Name == "UpperBound" || other.Name == "LowerBound") {
            _rigidBody.UndoMove();
            _rigidBody.Velocity.Y *= -1;
        }
    }
}
/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class PaddleScript : Component
{
	protected uint score;

    private RigidBody _rigidBody;
    private PhysicsComponent _physics;
    private GameObject _ball;
    private RenderComponent _ballRender;
	
    public override void Start() {
        score = 0;
        _rigidBody = Owner.GetComponent<RigidBody>();
        _physics = Owner.GetComponent<PhysicsComponent>();
        _ball = Owner.Game.FindGameObject("Ball");
        _ballRender = _ball.GetComponent<RenderComponent>();
    }

    public override void Update() {
        ////collisions & resolve
        //if (_physics.Intersects(_ball.Position, _ballRender.Size)) {
        //    if (_ball._rigidBody.Velocity.X > 0) {
        //        _paddle.ball.Owner.Position.X = _paddle.Owner.position.X - _paddle.ball.Owner.Size.X;
        //    } else if (_paddle.ball._rigidBody.Velocity.X < 0) {
        //        _paddle.ball.Owner.Position.X = _paddle.Owner.position.X + _paddle.ball.Owner.Size.X;
        //    }
        //    _paddle.ball._rigidBody.Velocity.X = -_paddle.ball._rigidBody.Velocity.X;
        //}

        //// collisions
        //if (_paddle.Owner.Position.Y < 0) _paddle.Owner.Position.Y = 0;
        //if (_paddle.Owner.Position.Y > 416) _paddle.Owner.Position.Y = 416;
    }

    public void IncScore() {
		score++;
	}
	
	public uint Score {
		get { 
			return score; 
		}
	}	
}



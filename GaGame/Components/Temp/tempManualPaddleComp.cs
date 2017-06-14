using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

class tempManualPaddleComp : Component {
    private RigidBody _rigidBody;
    private Paddle _paddle;
    float Speed = 5.0f;

    public tempManualPaddleComp() {
        //Debug.Assert(Owner != null); //--> makes World null??

        //_rigidBody = Owner.GetComponent<RigidBody>();
        //_paddle = (Paddle)Owner;
    }


    public override void Update() {
        //Lame
        if (_paddle == null) {
            _rigidBody = Owner.GetComponent<RigidBody>();
            _paddle = (Paddle)Owner;
        }

        // input

        _rigidBody.Velocity.Y = 0; // no move 
        if (Input.Key.Pressed(Keys.Up)) _rigidBody.Velocity.Y = -Speed;
        if (Input.Key.Pressed(Keys.Down)) _rigidBody.Velocity.Y = Speed;

        // move
        //position.Add(_rigidBody.Velocity); //rigidbody took over

        // collisions & resolve
        if (_paddle.Intersects(_paddle.ball.Position, _paddle.ball.Size)) {
            if (_paddle.ball._rigidBody.Velocity.X > 0) {
                _paddle.ball.Position.X = _paddle.position.X - _paddle.ball.Size.X;
            } else if (_paddle.ball._rigidBody.Velocity.X < 0) {
                _paddle.ball.Position.X = _paddle.position.X + _paddle.ball.Size.X;
            }
            _paddle.ball._rigidBody.Velocity.X = -_paddle.ball._rigidBody.Velocity.X;
        }

        // collisions
        if (_paddle.position.Y < 0) _paddle.position.Y = 0;
        if (_paddle.position.Y > 416) _paddle.position.Y = 416;
    }
}
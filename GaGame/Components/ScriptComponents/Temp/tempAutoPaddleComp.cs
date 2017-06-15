using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

class tempAutoPaddleComp : Component {
    private RigidBody _rigidBody;
    private PaddleScript _paddle;
    float Speed = 5.0f;

    public tempAutoPaddleComp() {
        //Debug.Assert(Owner != null); //--> makes World null??

        //_rigidBody = Owner.GetComponent<RigidBody>();
        //_paddle = (Paddle)Owner;
    }


    public override void Update() {
        //Lame
        if (_paddle == null) {
            _rigidBody = Owner.GetComponent<RigidBody>();
            _paddle = Owner.GetComponent<PaddleScript>();
        }

        // input
        _rigidBody.Velocity.Y = 0; // no move 

        if (_paddle.ball.Owner.Position.Y + 8 > _paddle.Owner.position.Y + 32 + 8) _rigidBody.Velocity.Y = +Speed;
        if (_paddle.ball.Owner.Position.Y + 8 < _paddle.Owner.position.Y + 32 - 8) _rigidBody.Velocity.Y = -Speed;


        // collisions & resolve
        if (_paddle.Intersects(_paddle.ball.Owner.Position, _paddle.ball.Owner.Size)) {
            if (_paddle.ball._rigidBody.Velocity.X > 0) {
                _paddle.ball.Owner.Position.X = _paddle.Owner.position.X - _paddle.ball.Owner.Size.X;
            } else if (_paddle.ball._rigidBody.Velocity.X < 0) {
                _paddle.ball.Owner.Position.X = _paddle.Owner.position.X + _paddle.ball.Owner.Size.X;
            }
            _paddle.ball._rigidBody.Velocity.X = -_paddle.ball._rigidBody.Velocity.X;
            _paddle.ball._rigidBody.Velocity.Y = (_paddle.ball.Owner.Center.Y - _paddle.Owner.Center.Y) / 32 + ((float)(Game.Random.NextDouble()) - 0.5f) * 10.0f; // curve randomly

        }

        // collisions
        if (_paddle.Owner.position.Y < 0) _paddle.Owner.position.Y = 0;
        if (_paddle.Owner.position.Y > 416) _paddle.Owner.position.Y = 416;
    }
}
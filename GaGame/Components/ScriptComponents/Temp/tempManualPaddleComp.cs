using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

class tempManualPaddleComp : Component {
    
    //private PaddleScript _paddle;
    

    public tempManualPaddleComp() {
        //Debug.Assert(Owner != null); //--> makes World null??

        //_rigidBody = Owner.GetComponent<RigidBody>();
        //_paddle = (Paddle)Owner;
    }


    public override void Update() {
        ////Lame
        //if (_paddle == null)            
        //    _paddle = Owner.GetComponent<PaddleScript>();

        ////collisions & resolve
        //if (_paddle.Intersects(_paddle.ball.Owner.Position, _paddle.ball.Owner.Size)) {
        //    if (_paddle.ball._rigidBody.Velocity.X > 0) {
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
}
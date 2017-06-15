using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class tempBallComp : Component {

    private RigidBody _rigidBody;
    private GameObject _ball;

    public tempBallComp() {

    }

    public override void Update() {
        //Lame
        if (_ball == null) {
            _rigidBody = Owner.GetComponent<RigidBody>();
            _ball = Owner;
        }

        // collisions & resolve
        // Y bounds reflect
        if (_ball.position.Y < 0) {
            _ball.position.Y = 0;
            _rigidBody.Velocity.Y = -_rigidBody.Velocity.Y;
        }
        if (_ball.position.Y > 480 - 16) { // note: non maintainable literals here, who did this
            _ball.position.Y = 480 - 16;
            _rigidBody.Velocity.Y = -_rigidBody.Velocity.Y;
        }

        // see game and paddles
    }
}

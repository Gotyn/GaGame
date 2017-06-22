using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class AutoPaddleScript : PaddleScript {
    override public void Update() {
        rigidBody_.Velocity.Y = 0; // no move 
        if (ball_.Position.Y + 8 > Owner.Position.Y + 32 + 8) rigidBody_.Velocity.Y = +Speed;
        if (ball_.Position.Y + 8 < Owner.Position.Y + 32 - 8) rigidBody_.Velocity.Y = -Speed;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class PaddleInput : Component {
    private float _speed = 5.0f;
    private RigidBody _rigidBody;


    public PaddleInput() { }

    public float Speed { get => _speed; set => _speed = value; }

    override public void Update() {
        //lame
        if (_rigidBody == null)
            _rigidBody = Owner.GetComponent<RigidBody>();

        // input
        _rigidBody.Velocity.Y = 0; // no move 
        if (Input.Key.Pressed(Keys.Up)) _rigidBody.Velocity.Y = -Speed;
        if (Input.Key.Pressed(Keys.Down)) _rigidBody.Velocity.Y = Speed;
    }

}
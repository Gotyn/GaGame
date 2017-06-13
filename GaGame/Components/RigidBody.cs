using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RigidBody : Component {
    private Vec2 _velocity = new Vec2();

    public override void Update() {
        Owner.position.Add(Velocity);
    }
    
    public void AddForce(Vec2 force) {
        Velocity += force;
    }

    public Vec2 Velocity { get => _velocity; set => _velocity = value; }
}
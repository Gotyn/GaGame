using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public class RigidBody : Component {
    private Vec2 _velocity = new Vec2();

    public override void Update() {
        Debug.Assert(Owner.Position != null);
        Owner.Position.Add(Velocity);
    }
    
    public void AddForce(Vec2 force) {
        Velocity += force;
    }

    public Vec2 Velocity { get => _velocity; set => _velocity = value; }
}
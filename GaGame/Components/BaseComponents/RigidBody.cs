using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public class RigidBody : Component {
    private bool _paused = false;
    private Vec2 _velocity = new Vec2();
    private Vec2 _previousPosition = new Vec2();

    public override void Start() {
        PauseEvent.Handlers += this.pauseHandler;
    }

    public override void Update() {
        Debug.Assert(Owner.Position != null);
        if (_paused) return;
        _previousPosition = new Vec2(Owner.Position.X, Owner.Position.Y);
        Owner.Position.Add(Velocity);
    }
    
    public void AddForce(Vec2 force) {
        Velocity += force;
    }

    private void pauseHandler(object sender, PauseEvent e) {
        Console.WriteLine("Pause Triggered");
        _paused = e.state == PauseState.Paused ? true : false;
    }

    public Vec2 Velocity { get => _velocity; set => _velocity = value; }
    public Vec2 PreviousPosition { get => _previousPosition; }
}
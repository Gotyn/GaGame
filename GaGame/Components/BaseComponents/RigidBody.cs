using System.Diagnostics;

public class RigidBody : Component {
    private bool _paused = false;
    private Vec2 _velocity = new Vec2();
    private Vec2 _previousPosition = new Vec2();

    public override void Start() {
        PauseEvent.Handlers += this.pauseHandler;
    }

    public override void Update() {
        Debug.Assert(Owner != null, "rigidbody appears to have no Owner set");
        Debug.Assert(Owner.Position != null, Owner.Name + " doesn't have a valid position");
        if (_paused) return;
        _previousPosition = new Vec2(Owner.Position.X, Owner.Position.Y);
        Owner.Position.Add(Velocity);
    }
    
    public void AddForce(Vec2 force) {
        Velocity += force;
    }

    private void pauseHandler(object sender, PauseEvent e) {
        _paused = e.state == PauseState.Paused ? true : false;
    }

    public void UndoMove() {
        Owner.Position = _previousPosition;
    }

    public Vec2 Velocity { get => _velocity; set => _velocity = value; }
    public Vec2 PreviousPosition { get => _previousPosition; }
}
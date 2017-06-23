using System.Windows.Forms;
using System.Diagnostics;

public class PaddleScript : Component
{
	private uint _score;
    private float _speed = 5.0f;
    protected RigidBody rigidBody_;
    protected GameObject ball_;
    private RenderComponent _ballRender;

    private Keys _up = Keys.Up;
    private Keys _down = Keys.Down;

    public float Speed { get => _speed; set => _speed = value; }
    public Keys Up { get => _up; set => _up = value; }
    public Keys Down { get => _down; set => _down = value; }


    public PaddleScript() { }
    public PaddleScript(Keys up, Keys down) {
        _up = up;
        _down = down;
    }

    public override void Start() {
        _score = 0;
        Debug.Assert(Owner.GetComponent<RigidBody>() != null, Owner.Name + " couldn't find rigidbody");
        rigidBody_ = Owner.GetComponent<RigidBody>();
        ball_ = Locator.Game.FindGameObject("Ball");
        _ballRender = ball_.GetComponent<RenderComponent>();
        RestartEvent.Handlers += this.RestartHandler;
    }

    override public void Update() {
        // input
        rigidBody_.Velocity.Y = 0; // no move 
        if (Input.Key.Pressed(Up)) rigidBody_.Velocity.Y = -Speed;
        if (Input.Key.Pressed(Down)) rigidBody_.Velocity.Y = Speed;
    }

    public void IncScore() {
		_score++;
	}
	
	public uint Score {
		get { return _score; }
	}	

    void RestartHandler(object sender, RestartEvent e) {
        _score = 0;
        Owner.Position.Y = 208;
    }

    override public void OnCollision(GameObject other) {
        if (other.Name == "UpperBound" || other.Name == "LowerBound") {
            rigidBody_.UndoMove();
        } 
    }
}



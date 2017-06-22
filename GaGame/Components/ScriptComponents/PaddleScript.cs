public class PaddleScript : Component
{
	private uint _score;
    private RigidBody _rigidBody;
    private GameObject _ball;
    private RenderComponent _ballRender;
	
    public override void Start() {
        _score = 0;
        _rigidBody = Owner.GetComponent<RigidBody>();
        _ball = Locator.Game.FindGameObject("Ball");
        _ballRender = _ball.GetComponent<RenderComponent>();
        RestartEvent.Handlers += this.RestartHandler;
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
            _rigidBody.UndoMove();
        } 
    }
}



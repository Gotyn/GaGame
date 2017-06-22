using System.Windows.Forms;

class GameManager : Component {
    private GameObject _ball;
    private PaddleScript _leftPaddle, _rightPaddle;
    private bool _paused;

    public override void Start() {
        _ball = Locator.Game.FindGameObject("Ball");
        _leftPaddle = Locator.Game.FindGameObject("LeftPaddle").GetComponent<PaddleScript>();
        _rightPaddle = Locator.Game.FindGameObject("RightPaddle").GetComponent<PaddleScript>();

        //Subscribe to events
        PauseEvent.Handlers += this.PauseHandler;
        CollisionEvent.Handlers += this.CollisionHandler;
    }

    public override void Update() {
        //GameOver check
        if (_leftPaddle.Score == 10 || _rightPaddle.Score == 10) {
            Locator.Game.SetGameState(GameState.GameOver);
        }

        //Pause feature
        if (Input.Key.Enter(Keys.P)) {
            Locator.EventManager.AddEvent(new PauseEvent(_paused ? PauseState.Unpaused : PauseState.Paused));
        }
    }

    void PauseHandler(object sender, PauseEvent e) {
        _paused = e.state == PauseState.Paused ? true : false;
    }

    void CollisionHandler(object sender, CollisionEvent e) { //only needs to check for one combination because of the double dispatching.
        //Check if ball got scored and increase score accordingly.
        if (e.A_Collidee.Name == "Ball") {
            if (e.B_Collidee.Name == "LeftBound") {
                _rightPaddle.IncScore();
                _ball.GetComponent<BallScript>().Reset();
            } 
            else if (e.B_Collidee.Name == "RightBound") {
                _leftPaddle.IncScore();
                _ball.GetComponent<BallScript>().Reset();
            }
        } 
    }
}

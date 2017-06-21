using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class GameManager : Component {
    private GameObject _ball;
    private PaddleScript _leftPaddle, _rightPaddle;
    private bool _paused;

    public override void Update() {
        //ScoreBounds
        if (_ball.Position.X < 0) {
            _rightPaddle.IncScore();
            _ball.GetComponent<BallScript>().Reset();
        }
        if (_ball.Position.X > 640 - 16) { // note: bad literals detected
            _leftPaddle.IncScore();
            _ball.GetComponent<BallScript>().Reset();
        }

        if (_leftPaddle.Score == 10 || _rightPaddle.Score == 10) {
            Locator.Game.SetGameState(GameState.GameOver);
        }

        if (Input.Key.Enter(Keys.P)) {
            Locator.EventManager.AddEvent(new PauseEvent(_paused ? PauseState.Unpaused : PauseState.Paused));
        }


    }

    void PauseHandler(object sender, PauseEvent e) {
        _paused = e.state == PauseState.Paused ? true : false;
    }

    public override void Start() {
        _ball = Locator.Game.FindGameObject("Ball");
        _leftPaddle = Locator.Game.FindGameObject("LeftPaddle").GetComponent<PaddleScript>();
        _rightPaddle = Locator.Game.FindGameObject("RightPaddle").GetComponent<PaddleScript>();
        PauseEvent.Handlers += this.PauseHandler;
    }
}

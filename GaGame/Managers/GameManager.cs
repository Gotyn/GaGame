using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GameManager : Component {
    private GameObject _ball;
    private PaddleScript _leftPaddle, _rightPaddle;

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
    }


    public override void Start() {
        _ball = Locator.Game.FindGameObject("Ball");
        _leftPaddle = Locator.Game.FindGameObject("LeftPaddle").GetComponent<PaddleScript>();
        _rightPaddle = Locator.Game.FindGameObject("RightPaddle").GetComponent<PaddleScript>();

    }
}

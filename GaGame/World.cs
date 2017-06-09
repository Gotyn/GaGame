using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

class World : GameObject {
    private Text leftScore;
    private Text rightScore;

    public Ball ball;
    private Paddle leftPaddle;
    private Paddle rightPaddle;
    private Booster booster1;
    private Booster booster2;

    //private List<GameObject> gameObjectList = new List<GameObject>();

    public World() {
        _name = "World";

        ball = new Ball("Ball", "ball.png"); // orbitting the window centre
        ball.Parent = this;

        leftPaddle = new AutoPaddle("Left", 10, 208, "paddle.png", ball);
        leftPaddle.Parent = this;

        rightPaddle = new AutoPaddle("Right", 622, 208, "paddle.png", ball);
        rightPaddle.Parent = this;

        leftScore = new Text("LeftScore", 320 - 20 - 66, 10, "digits.png", leftPaddle);
        leftScore.Parent = this;


        rightScore = new Text("RightScore", 320 + 20, 10, "digits.png", rightPaddle);
        rightScore.Parent = this;

        booster1 = new Booster("Booster", 304, 96, "booster.png", ball);
        booster1.Parent = this;

        booster2 = new Booster("Booster", 304, 384, "booster.png", ball);
        rightScore.Parent = this;
    }


    public void Update(Graphics pGraphics) {

    
        #region "Draw Update()"
        ball.Update(pGraphics);
        leftPaddle.Update(pGraphics);
        rightPaddle.Update(pGraphics);
        booster1.Update(pGraphics);
        booster2.Update(pGraphics);

        leftScore.Update(pGraphics);
        rightScore.Update(pGraphics);

        #endregion

        if (ball.Position.X < 0) {
            rightPaddle.IncScore();
            ball.Reset();
        }
        if (ball.Position.X > 640 - 16) { // note: bad literals detected
            leftPaddle.IncScore();
            ball.Reset();
        }

        

    }

}
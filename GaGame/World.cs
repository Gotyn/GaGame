using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

class World {
    private Text leftScore;
    private Text rightScore;

    public Ball ball;
    private Paddle leftPaddle;
    private Paddle rightPaddle;
    private Booster booster1;
    private Booster booster2;

    private List<GameObject> gameObjectList = new List<GameObject>();

    public World() {
        ball = new Ball("Ball", "ball.png"); // orbitting the window centre
        leftPaddle = new AutoPaddle("Left", 10, 208, "paddle.png", ball);
        rightPaddle = new AutoPaddle("Right", 622, 208, "paddle.png", ball);
        leftScore = new Text("LeftScore", 320 - 20 - 66, 10, "digits.png", leftPaddle);
        rightScore = new Text("RightScore", 320 + 20, 10, "digits.png", rightPaddle);
        booster1 = new Booster("Booster", 304, 96, "booster.png", ball);
        booster2 = new Booster("Booster", 304, 384, "booster.png", ball);
    }


    public void Update(Graphics pGraphics) {
        
        #region "Normal Update()"
        ball.Update();
        leftPaddle.Update();
        rightPaddle.Update();
        booster1.Update();
        booster2.Update();
        leftScore.Update();
        rightScore.Update();
        #endregion

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
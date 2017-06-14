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
    public Paddle leftPaddle;
    public Paddle rightPaddle;
    private Booster booster1;
    private Booster booster2;

    public World() {
        _name = "World";

        ball = new Ball("Ball", "ball.png", this); // orbitting the window centre
        
        leftPaddle = new AutoPaddle("Left", 10, 208, "paddle.png", ball, this);
        rightPaddle = new AutoPaddle("Right", 622, 208, "paddle.png", ball, this);

        //leftPaddle = new Paddle("Left", 10, 208, "paddle.png", ball, this);
        //rightPaddle = new Paddle("Right", 622, 208, "paddle.png", ball, this);

        //leftPaddle = new CurvedPaddle("Left", 10, 208, "paddle.png", ball, this);
        //rightPaddle = new CurvedPaddle("Right", 622, 208, "paddle.png", ball, this);

        leftScore = new Text("LeftScore", 320 - 20 - 66, 10, "digits.png", leftPaddle, this);
        rightScore = new Text("RightScore", 320 + 20, 10, "digits.png", rightPaddle, this);
        booster1 = new Booster("Booster", 304, 96, "booster.png", ball, this);
        booster2 = new Booster("Booster", 304, 384, "booster.png", ball, this);
    }

    override public void Update() {
        base.Update();

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
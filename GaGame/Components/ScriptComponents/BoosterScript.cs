using System;

public class BoosterScript : Component { 
	public bool active = true;
	public BallScript ball;
	
    public override void Start() {
        ball = Locator.Game.FindGameObject("Ball").GetComponent<BallScript>();
    }

    public void DeBoost(Object sender, Time.TimeoutEvent timeout) {
		ball.DeBoost();
		active = true;
	}	

    public override void OnCollision(GameObject other) {
        if (active && other.Name == "Ball") {
            active = false;
            ball.Boost();
            Time.Timeout("Deboosting", 0.5f, DeBoost);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PhysicsComponent {
    private GameObject _gameObject;

    public void Update(GameObject gameObject, Ball ball) {
        _gameObject = gameObject;

        // move
        gameObject.position.Add(gameObject.velocity);

        // collisions & resolve
        if (Intersects(ball.Position, ball.Size)) {
            if (ball.Velocity.X > 0) {
                ball.Position.X = gameObject.position.X - ball.Size.X;
            } else if (ball.Velocity.X < 0) {
                ball.Position.X = gameObject.position.X + gameObject.Size.X;
            }
            ball.Velocity.X = -ball.Velocity.X;
        }

        // collisions
        if (gameObject.position.Y < 0) gameObject.position.Y = 0;
        if (gameObject.position.Y > 416) gameObject.position.Y = 416;
    }

    public bool Intersects(Vec2 otherPosition, Vec2 otherSize) {
        return
           _gameObject.position.X < otherPosition.X + otherSize.X && _gameObject.position.X + _gameObject.Size.X > otherPosition.X &&
           _gameObject.position.Y < otherPosition.Y + otherSize.Y && _gameObject.position.Y + _gameObject.Size.Y > otherPosition.Y;
    }
}

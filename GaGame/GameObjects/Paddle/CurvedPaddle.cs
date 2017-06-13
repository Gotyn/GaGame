/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 6:09 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;

public class CurvedPaddle : Paddle
{	
	
	public CurvedPaddle( string pName, float pX, float pY, string pImageFile, Ball pBall, GameObject pParent = null ) : base( pName, pX, pY, pImageFile, pBall, pParent ) {	}

    public override void Update() {
        // input

        _rigidBody.Velocity.Y = 0; // no move 
        if (Input.Key.Pressed(Keys.Up)) _rigidBody.Velocity.Y = -Speed;
        if (Input.Key.Pressed(Keys.Down)) _rigidBody.Velocity.Y = Speed;

        // move
        position.Add(_rigidBody.Velocity);

        // collisions & resolve
        if (Intersects(ball.Position, ball.Size)) {
            if (ball._rigidBody.Velocity.X > 0) {
                ball.Position.X = position.X - ball.Size.X;
            } else if (ball._rigidBody.Velocity.X < 0) {
                ball.Position.X = position.X + Size.X;
            }
            ball._rigidBody.Velocity.X = -ball._rigidBody.Velocity.X;
            ball._rigidBody.Velocity.Y = (ball.Center.Y - Center.Y) / 64 + ((float)(Game.Random.NextDouble()) - 0.5f) / 1.0f;
        }

        // collisions
        if (position.Y < 0) position.Y = 0;
        if (position.Y > 416) position.Y = 416;
    }
}
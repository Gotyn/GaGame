/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 6:09 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;

public class AutoPaddle : Paddle
{
	public AutoPaddle( string pName, float pX, float pY, string pImageFile, Ball pBall )
		: base( pName, pX, pY, pImageFile, pBall ) 
	{
	}
	
	override public void Update( Graphics graphics )
	{
		// input
		
		velocity.Y = 0; // no move 
		if ( ball.Position.Y+8 > position.Y+32 + 8 ) velocity.Y = +Speed;
		if ( ball.Position.Y+8 < position.Y+32 - 8 ) velocity.Y = -Speed;
		
		// move
		position.Add( velocity );
		
		// collisions & resolve
		if( Intersects( ball.Position, ball.Size ) ) {
			if( ball.Velocity.X > 0 ) {
				ball.Position.X = position.X - ball.Size.X;
			} else if( ball.Velocity.X < 0 ) {
				ball.Position.X = position.X + Size.X;
			}
			ball.Velocity.X = -ball.Velocity.X;
			ball.Velocity.Y = ( ball.Center.Y - Center.Y ) / 32 + ( (float)(Game.Random.NextDouble())-0.5f ) * 10.0f; // curve randomly
		
		}
		
		// collisions
		if( position.Y < 0 ) position.Y = 0;
		if( position.Y > 416 ) position.Y = 416;
		
		// render
		graphics.DrawImage( image, position.X, position.Y );
	}	
}


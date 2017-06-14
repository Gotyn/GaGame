﻿/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class Booster : GameObject
{
	public bool active = true;
	public Ball ball;
	
	public Booster( string pName, float pX, float pY, string pImageFile, Ball pBall, GameObject pParent = null ) : base (pName, pParent)
	{
		image = Image.FromFile( pImageFile );
		position = new Vec2( pX, pY );
		
		ball = pBall;

        AddComponent(new tempBoosterComp());
	}

	// Event handlers

	public void DeBoost( Object sender,  Time.TimeoutEvent timeout )
	{
		ball.DeBoost();
		active = true;
		//Console.WriteLine( "Deboosting "+name );
	}	

	// Tools
	public bool Intersects( Vec2 otherPosition, Vec2 otherSize ) {
		return
		    this.position.X < otherPosition.X+otherSize.X && this.position.X + this.Size.X > otherPosition.X &&
		    this.position.Y < otherPosition.Y+otherSize.Y && this.position.Y + this.Size.Y > otherPosition.Y;
	}
	
	
	public void Reset() 
	{
		position.X = 320-8;
		position.Y = 240-8;
	}
	
}


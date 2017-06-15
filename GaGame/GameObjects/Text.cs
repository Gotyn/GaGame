/*
 * User: Eelco
 * Date: 5/16/2017
 * Time: 9:05 AM
 */
using System;
using System.Diagnostics;
using System.Drawing;

public class Text : GameObject
{
	private string text;
	
	public Paddle paddle;
	
	
	public Text(Game pGame, string pName, float pX, float pY, string pImageFile, Paddle pPaddle) : base(pGame, pName) {
		image = Image.FromFile( pImageFile );
		position = new Vec2( pX, pY );
		text = "0";
		paddle = pPaddle;

        AddComponent(new tempTextComp());
        AddComponent(new RenderComponent());
    }

	public string Value {
		get {
			return text;
		}
		set {
			text = value;
		}
	}
}


/*
 * User: Eelco
 * Date: 5/16/2017
 * Time: 9:05 AM
 */
using System;
using System.Diagnostics;
using System.Drawing;

public class TextScript : Component
{
	private string text;
	
	public PaddleScript paddle;
	
	
	public TextScript(Game pGame, string pName, float pX, float pY, string pImageFile, GameObject pPaddle) {
        Owner.Position = new Vec2( pX, pY );
		text = "0";
		paddle = pPaddle.GetComponent<PaddleScript>();

        Owner.AddComponent(new tempTextComp());
        Owner.AddComponent(new RenderComponent());
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


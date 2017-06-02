/*
 * User: Eelco
 * Date: 5/16/2017
 * Time: 9:05 AM
 */
using System;
using System.Diagnostics;
using System.Drawing;

public class Text
{
	private string name;
	private Image image;
	private Vec2 position = null;
	private string text;
	
	private Paddle paddle;
	
	
	public Text( string pName, float pX, float pY, string pImageFile, Paddle pPaddle )
	{
		name = pName;
		image = Image.FromFile( pImageFile );
		position = new Vec2( pX, pY );
		text = "0";
		paddle = pPaddle;
	}
	
	virtual public void Update( Graphics graphics )
	{
		Debug.Assert(graphics != null );
		Debug.Assert(paddle != null );
		// input

		// move
		
		// collisions & resolve
		
		// render
		int digits = 2;
		string score = "000"+paddle.Score.ToString();
		for( int d=0; d<digits; d++ ) { // 3 digits left to right
			int digit = score[ score.Length-digits + d ] - 48; // '0' => 0 etc
			Rectangle rect = new Rectangle( digit * image.Width/10, 0, image.Width/10, image.Height );
			graphics.DrawImage( image, position.X + d*image.Width/10, position.Y, rect, GraphicsUnit.Pixel );
		}
	}		
	
	public string Value {
		get {
			return text;
		}
		set {
			text = value;
		}
	}
	
	public Vec2 Position {
		get { 
			return position;
		}
	}	
}


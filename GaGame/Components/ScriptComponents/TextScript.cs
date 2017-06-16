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
	private string _text;
	
	private GameObject _paddle;
	
	public TextScript() {}

    public override void Start() {
        _text = "0";
    }

    public override void Update() {
        //Debug.Assert(graphics != null);
        Debug.Assert(_paddle != null);

        // input

        // move

        // collisions & resolve

        // render
        int digits = 2;
        string score = "000" + _paddle.GetComponent<PaddleScript>().Score.ToString();
        for (int d = 0; d < digits; d++) { // 3 digits left to right
            int digit = score[score.Length - digits + d] - 48; // '0' => 0 etc
            //Rectangle rect = new Rectangle(digit * _text.paddle.image.Width / 10, 0, _text.paddle.image.Width / 10, _text.paddle.image.Height);
            //graphics.DrawImage(image, position.X + d * image.Width / 10, position.Y, rect, GraphicsUnit.Pixel);
        }
    }



    public string Value {
		get {
			return _text;
		}
		set {
			_text = value;
		}
	}

    public GameObject Paddle { get => _paddle; set => _paddle = value; }
}


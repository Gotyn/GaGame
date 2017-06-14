using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;


class tempTextComp : Component {
    private Text _text;


    public override void Update() {
        if (_text == null) {
            _text = (Text)Owner;
        }
        
        //Debug.Assert(graphics != null);
        Debug.Assert(_text.paddle != null);
        
        // input

        // move

        // collisions & resolve

        // render
        int digits = 2;
        string score = "000" + _text.paddle.Score.ToString();
        for (int d = 0; d < digits; d++) { // 3 digits left to right
            int digit = score[score.Length - digits + d] - 48; // '0' => 0 etc
            Rectangle rect = new Rectangle(digit * _text.paddle.image.Width / 10, 0, _text.paddle.image.Width / 10, _text.paddle.image.Height);
            //graphics.DrawImage(image, position.X + d * image.Width / 10, position.Y, rect, GraphicsUnit.Pixel);
        }
    }
}
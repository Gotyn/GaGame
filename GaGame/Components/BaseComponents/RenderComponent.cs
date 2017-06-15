using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Collections;

public class RenderComponent : Component {
    private GameObject _drawable;
    private Image _image;

    public Image Image { get => _image; set => _image = value; }

    public RenderComponent() {
    }

    public RenderComponent(Image image) {
        Image = image;
    }

    override public void Update() {
        if (_drawable == null) {
            _drawable = Owner;
            _drawable.Game.AddToDrawables(_drawable);
        }
    }

    public void Draw(Graphics graphics) {
        //Debug.Assert(graphics != null);
        //Debug.Assert(Image != null);
        //graphics.DrawImage(_image, _drawable.position.X, _drawable.position.Y);
    }
}
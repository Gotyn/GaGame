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

    public RenderComponent() {
    }


    override public void Update() {
        if (_drawable == null) {
            _drawable = Owner;
            _drawable.Game.AddToDrawables(_drawable);
        }

        Draw(_drawable.Game.Graphics);

    }

    public void Draw(Graphics graphics) {
        Debug.Assert(graphics != null);
        graphics.DrawImage(_drawable.image, _drawable.position.X, _drawable.position.Y);
    }
}
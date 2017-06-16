using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Collections;

public class RenderComponent : Component {
    private Image _image;
    bool set = false;

    public Image Image { get => _image; set => _image = value; }

    public override void Start() {
        Owner.Game.AddToDrawables(Owner);
        
    }

    public RenderComponent() {}
    public RenderComponent(Image image) {
        Image = image;
    }

    override public void Update() {
        if (!set) {
            Owner.Size = Size;
            set = true;
        }
    }

    public void Draw(Graphics graphics) {
        Debug.Assert(graphics != null);
        Debug.Assert(Image != null);
        graphics.DrawImage(_image, Owner.Position.X, Owner.Position.Y);
    }

    public Vec2 Size {
        get {
            Debug.Assert(_image != null);
            return new Vec2(_image.Width, _image.Height);
        }
    }

    public Vec2 Center {
        get {
            return Owner.Position + 0.5f * Size;
        }
    }

}
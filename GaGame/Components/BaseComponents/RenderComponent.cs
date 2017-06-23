using System.Drawing;
using System.Diagnostics;

public class RenderComponent : Component {
    private Image _image;
    private bool _enabled = true;

    public Image Image { get => _image; set => _image = value; }
    public Vec2 Center { get => Owner.Position + 0.5f * Size; }
    public bool Enabled { get => _enabled; set => _enabled = value; }

    public override void Start() {
        Debug.Assert(Owner != null, "Owner not set on a RenderComponent");
        Owner.Size = Size;
        Locator.Game.AddToDrawables(this);
    }

    public RenderComponent() {}
    public RenderComponent(Image image) {
        Image = image;
    }

    virtual public void Draw(Graphics graphics, float timeIntoNextFrame) {
        Debug.Assert(graphics != null, "graphics is NULL (" + Owner.Name + ")");
        Debug.Assert(Image != null, "image is NULL (" + Owner.Name + ")");

        RigidBody rigidBody = Owner.GetComponent<RigidBody>();
        if (_enabled) {
            if (rigidBody != null) {
                graphics.DrawImage(_image, Owner.Position.X + rigidBody.Velocity.X * timeIntoNextFrame, Owner.Position.Y + rigidBody.Velocity.Y * timeIntoNextFrame);
            } else {
                graphics.DrawImage(_image, Owner.Position.X, Owner.Position.Y);
            }
        }
    }

    public Vec2 Size {
        get {
            Debug.Assert(_image != null, "Image not set. (" + Owner.Name + ")");
            return new Vec2(_image.Width, _image.Height);
        }
    }
}
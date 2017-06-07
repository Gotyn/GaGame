using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

public abstract class GameObject {
    public string name = "GameObject";
    public Vec2 position = new Vec2();
    public Vec2 velocity = new Vec2();
    public Image image;


    public GameObject(string pName, Vec2 pPosition, Vec2 pVelocity, String pImageFile) {
        name = pName;
        image = Image.FromFile(pImageFile);
        position = pPosition;
        velocity = pVelocity;
    }

    public GameObject(string pName, float positionX, float positionY, float velocityX, float velocityY, String pImageFile) {  //overloading for floats
        name = pName;
        image = Image.FromFile(pImageFile);
        position = new Vec2(positionX, positionY);
        velocity = new Vec2(velocityX, velocityY);

    }

    virtual public void Update(Graphics graphics) {

    }

    public Vec2 Center {
        get {
            return position + 0.5f * Size;
        }
    }
    public Vec2 Position {
        get {
            return position;
        }
    }
    public Vec2 Size {
        get {
            return new Vec2(image.Width, image.Height);
        }
    }
    public Vec2 Velocity {
        get {
            return velocity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

public class RenderComponent {
    public void Update(GameObject gameObject, Graphics graphics) {
        graphics.DrawImage(gameObject.image, gameObject.position.X, gameObject.position.Y);
    }
}

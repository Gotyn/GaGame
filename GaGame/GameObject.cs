using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

abstract public class GameObject : Object {
    private GameObject _parent;

    public Image image;
    public Vec2 position;


    abstract public void Update();
}
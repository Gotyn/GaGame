using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public class PhysicsComponent : Component {
    private Vec2 _size;

    public override void Start() {
        _size = Owner.GetComponent<RenderComponent>().Size;
    }

    public override void Update() {
        
    }


    public bool Intersects(Vec2 otherPosition, Vec2 otherSize) {
        Debug.Assert(_size != null);
        return
            Owner.Position.X < otherPosition.X + otherSize.X && Owner.Position.X + _size.X > otherPosition.X &&
            Owner.Position.Y < otherPosition.Y + otherSize.Y && Owner.Position.Y + _size.Y > otherPosition.Y;
    }
}
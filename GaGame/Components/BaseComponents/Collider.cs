using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Collider : Component {
    public override void Start() {
        Locator.CollisionManager.AddCollidingObject(Owner);
    }
}
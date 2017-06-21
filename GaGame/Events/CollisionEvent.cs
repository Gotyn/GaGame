using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CollisionEvent : Event {
    public static EventHandler<CollisionEvent> Handlers;
    public readonly GameObject A_Collidee;
    public readonly GameObject B_Collidee;

    public CollisionEvent(GameObject A, GameObject B) {
        A_Collidee = A;
        B_Collidee = B;
    }

    public override void Deliver() {
        Handlers?.Invoke(this, this);
    }


}
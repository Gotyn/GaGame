using System;

class CollisionEvent : Event {
    public static EventHandler<CollisionEvent> Handlers;
    public readonly GameObject A_Collidee, B_Collidee;

    public CollisionEvent(GameObject A, GameObject B) {
        A_Collidee = A;
        B_Collidee = B;
    }

    public override void Deliver() {
        Handlers?.Invoke(this, this);
        A_Collidee.OnCollision(B_Collidee);
    }
}
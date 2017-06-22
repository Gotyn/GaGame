class Collider : Component {
    public override void Start() {
        Locator.CollisionManager.AddCollidingObject(Owner);
    }
}
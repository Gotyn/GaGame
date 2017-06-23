using System.Diagnostics;

class Collider : Component {
    public override void Start() {
        Debug.Assert(Locator.CollisionManager != null, "Locator couldn't find CollisionManager. Called from: " + Owner.Name);
        Locator.CollisionManager.AddCollidingObject(Owner);
    }
}
using System.Collections.Generic;

class CollisionManager {
    private Game _game;
    private List<GameObject> _colliders = new List<GameObject>();

    public CollisionManager(Game game) {
        _game = game;
    }

    public void Update() {
        //Check all colliders against each other.
        for (int i = _colliders.Count - 1; i >= 0 ; i--) {
            for (int j = i - 1; j >= 0; j--) {
                CheckCollision(_colliders[i], _colliders[j]);
            }
        }
    }

    public void CheckCollision(GameObject a, GameObject b) {
        if (a.Position.X < b.Position.X + b.Size.X && a.Position.X + a.Size.X > b.Position.X &&
            a.Position.Y < b.Position.Y + b.Size.Y && a.Position.Y + a.Size.Y > b.Position.Y) {

            //raise an event about this epic collision
            Locator.EventManager.AddEvent(new CollisionEvent(a, b));
            Locator.EventManager.AddEvent(new CollisionEvent(b, a));
        }
    }

    public void AddCollidingObject(GameObject collider) {
        _colliders.Add(collider);
    }
}

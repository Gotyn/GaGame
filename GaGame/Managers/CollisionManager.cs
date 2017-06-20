using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CollisionEvent {
    public GameObject A_Collidee, B_Collidee;

    public CollisionEvent(GameObject A, GameObject B) {
        A_Collidee = A;
        B_Collidee = B;
    }
}

class CollisionManager {
    private Game _game;

    static int MAX_PENDING = 20;
    private CollisionEvent[] _collisionEvents = new CollisionEvent[MAX_PENDING];
    
    private List<GameObject> _colliders = new List<GameObject>();
    private int _head, _tail;

    public CollisionManager(Game game) {
        _game = game;
        _head = 0;
        _tail = 0;
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

            //Resolve Collision
            RigidBody aRigidBody = a.GetComponent<RigidBody>();
            //a.Position = aRigidBody.PreviousPosition;
            aRigidBody.Velocity.X *= -1;

            RigidBody bRigidBody = b.GetComponent<RigidBody>();
            //b.Position = bRigidBody.PreviousPosition;
            bRigidBody.Velocity.X *= -1;

            //raise an event about this epic collision
            addCollisionEvent(a, b);
            

        }
    }

    private void addCollisionEvent(GameObject a, GameObject b) {
        _collisionEvents[_tail] = new CollisionEvent(a, b);
        _tail = (_tail + 1) % MAX_PENDING;
    }

    public void DoCollisionEvents() {
        while (_tail != _head) {
            _collisionEvents[_head].A_Collidee.OnCollision(_collisionEvents[_head].B_Collidee);
            _collisionEvents[_head].B_Collidee.OnCollision(_collisionEvents[_head].A_Collidee);

            _head = (_head + 1) % MAX_PENDING;
        }
    }

    public void AddCollidingObject(GameObject collider) {
        _colliders.Add(collider);
    }
}

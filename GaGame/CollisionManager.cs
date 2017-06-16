using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CollisionManager {
    private Game _game;

    public CollisionManager(Game game) {
        _game = game;
    }

    public void Update() {
    }

    public void CheckCollision(GameObject A, GameObject B) {
        //Console.WriteLine("A.Position.X < B.Position.X + B.Size.X -- " + (A.Position.X < B.Position.X + B.Size.X));
        //Console.WriteLine("A.Position.X + A.Size.X > B.Position.X -- " + (A.Position.X + A.Size.X > B.Position.X));
        //Console.WriteLine("A.Position.Y < B.Position.Y + B.Size.Y -- " + (A.Position.Y < B.Position.Y + B.Size.Y));
        //Console.WriteLine("A.Position.Y + A.Size.Y > B.Position.Y -- " + (A.Position.Y + A.Size.Y > B.Position.Y));
        //Console.WriteLine("------------");

        if (A.Position.X < B.Position.X + B.Size.X && A.Position.X + A.Size.X > B.Position.X &&
            A.Position.Y < B.Position.Y + B.Size.Y && A.Position.Y + A.Size.Y > B.Position.Y) {

            //Resolve Collision
            RigidBody aRigidBody = A.GetComponent<RigidBody>();
            A.Position = aRigidBody.PreviousPosition;
            aRigidBody.Velocity.X *= -1;

            RigidBody bRigidBody = B.GetComponent<RigidBody>();
            B.Position = bRigidBody.PreviousPosition;
            bRigidBody.Velocity.X *= -1;
        }
    }

}

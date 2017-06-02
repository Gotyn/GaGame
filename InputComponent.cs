using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

public class InputComponent {
    public void Update(GameObject gameObject, bool auto = false) {
        gameObject.velocity.Y = 0; // no move 

        if (!auto) {
            if (Input.Key.Pressed(Keys.Up)) gameObject.velocity.Y = -5;
            if (Input.Key.Pressed(Keys.Down)) gameObject.velocity.Y = 5;
        } 
        //else {
        //    if (ball.Position.Y + 8 > position.Y + 32 + 8) velocity.Y = +Speed;
        //    if (ball.Position.Y+8 < position.Y+32 - 8 ) velocity.Y = -Speed;
        //}
    }
}

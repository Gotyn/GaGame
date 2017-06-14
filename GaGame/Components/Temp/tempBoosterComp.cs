using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class tempBoosterComp : Component {
    private Booster _booster;

    public override void Update() {
        if (_booster == null) {
            _booster = (Booster) Owner;
        }


        if (_booster.active && _booster.Intersects(_booster.ball.Position, _booster.ball.Size)) {
            _booster.active = false;
            _booster.ball.Boost();
            Time.Timeout("Deboosting", 0.5f, _booster.DeBoost);
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class RestartEvent : Event {

    public static EventHandler<RestartEvent> Handlers;

    public override void Deliver() {
        Handlers?.Invoke(this, this);
    }
}
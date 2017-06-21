using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartEvent : Event {
    public static EventHandler<StartEvent> Handlers;
    public readonly Component component;

    public StartEvent(Component pComponent) {
        component = pComponent;
    }


    public override void Deliver() {
        component.Start();
        Handlers?.Invoke(this, this);
    }
}

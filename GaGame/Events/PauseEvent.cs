using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PauseState { Paused, Unpaused }

class PauseEvent : Event {
    public static EventHandler<PauseEvent> Handlers;
    public readonly PauseState state;

    public PauseEvent(PauseState pState) {
        state = pState;
    }

    public override void Deliver() {
        Handlers?.Invoke(this, this);
    }
}
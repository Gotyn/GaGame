using System;

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
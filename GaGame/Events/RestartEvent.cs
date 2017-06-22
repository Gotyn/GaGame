using System;

class RestartEvent : Event {

    public static EventHandler<RestartEvent> Handlers;

    public override void Deliver() {
        Handlers?.Invoke(this, this);
    }
}
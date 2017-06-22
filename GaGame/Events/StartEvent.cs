using System;

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

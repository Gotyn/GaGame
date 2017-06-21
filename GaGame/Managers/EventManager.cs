using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

class EventManager {
    private List<Event> _events = new List<Event>();

    public void AddEvent(Event e) {
        Debug.Assert(e != null, "Null event!");
        _events.Add(e);
    }

    public void DeliverEvents() {
        foreach (Event e in _events) {
            e.Deliver();
        }
        _events.Clear();
    }

}
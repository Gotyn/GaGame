using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Locator {

    private static EventManager _eventManager;
    public static EventManager EventManager {
        get {
            //singleton
            if (_eventManager == null) _eventManager = new EventManager();
            return _eventManager;
        }
    }

    public static Game Game { get; set; }
    public static CollisionManager CollisionManager { get; set; }
    
}
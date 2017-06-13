using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract public class Component {
    private GameObject _owner;

    public Component() { }

    virtual public void Update() { }

    public GameObject Owner { get => _owner; set => _owner = value; }
}
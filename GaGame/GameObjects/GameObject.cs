using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

public class GameObject : Object {
    private GameObject _parent;

    protected string _name = null;
    public Image image; //shouldnt be part of gameobject maybe.
    public Vec2 position = null;

    public List<GameObject> childrenList = new List<GameObject>();

    public GameObject() {
    }

    public GameObject(string name) {
        _name = name;
    }

    virtual public void Update() {

    }

    public void SetParent(GameObject parent) {
        _parent = parent;
        parent.childrenList.Add(this); //add self to list of children from parent.
    }

    public void UpdateChildren() {
        foreach (GameObject child in childrenList) {
            child.UpdateChildren();
        }
    }

    public void ListChildren() {
        foreach (GameObject child in childrenList) {
            Console.WriteLine(child._name);
        }
    }


}
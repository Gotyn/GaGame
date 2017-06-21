using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

public class GameObject : Object {
    private string name = "New GameObject";
    private Vec2 _position = null;
    private Vec2 _size = null;

    public List<Component> componentList = new List<Component>();


    public GameObject() {
        Locator.Game.AddToGameObjects(this);
    }

    public GameObject(string pName) : this () {
        Name = pName;
    }

    public GameObject(string pName, Vec2 pPosition) : this(pName) {
        Position = pPosition;
    }

    public void Update() {
        foreach (Component component in componentList) {
            component.Update();
        }
    }

    public string Name { get => name; set => name = value; }
    public Vec2 Position { get => _position; set => _position = value; }
    public Vec2 Size { get => _size; set => _size = value; }

    public void AddComponent(Component component) {
        component.Owner = this;
        componentList.Add(component);
        Locator.EventManager.AddEvent(new StartEvent(component));
    }

    public T AddComponent<T>() where T : Component, new() {
        T component = new T();
        component.Owner = this;
        componentList.Add(component);
        Locator.EventManager.AddEvent(new StartEvent(component));
        return component;
    }

    public void RemoveComponent(Component component) {
        component.Owner = null;
        componentList.Remove(component);
    }

    public void RemoveComponentOfType(Type type) {
        for (int i = componentList.Count - 1; i >= 0; i--) {
            if (componentList[i].GetType() == type)
                RemoveComponent(componentList[i]);
        }
    }

    public T GetComponent<T>() where T:Component {
        foreach (Component component in componentList) {
            if (component.GetType() == typeof(T))
                return (T) component;
        }
        return null;
    }

    public List<Component> GetComponents(Type type) {
        List<Component> returnList = new List<Component>();
        foreach (Component component in componentList) {
            if (component.GetType() == type)
                returnList.Add(component);
        }
        return returnList;
    }

    public void OnCollision(GameObject other) {
        Console.WriteLine(Name + " just had an epic collision with " + other.Name);
    }
}
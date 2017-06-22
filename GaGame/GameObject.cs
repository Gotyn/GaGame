using System;
using System.Collections.Generic;

public class GameObject : Object {
    private string name = "New GameObject";
    private Vec2 _position = null;
    private Vec2 _size = null;

    private List<Component> _componentList = new List<Component>();

    public string Name { get => name; set => name = value; }
    public Vec2 Position { get => _position; set => _position = value; }
    public Vec2 Size { get => _size; set => _size = value; }

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
        foreach (Component component in _componentList) {
            component.Update();
        }
    }

    public void AddComponent(Component component) {
        component.Owner = this;
        _componentList.Add(component);
        Locator.EventManager.AddEvent(new StartEvent(component));
    }

    public T AddComponent<T>() where T : Component, new() {
        T component = new T();
        component.Owner = this;
        _componentList.Add(component);
        Locator.EventManager.AddEvent(new StartEvent(component));
        return component;
    }

    public void RemoveComponent(Component component) {
        component.Owner = null;
        _componentList.Remove(component);
    }

    public void RemoveComponentOfType(Type type) {
        for (int i = _componentList.Count - 1; i >= 0; i--) {
            if (_componentList[i].GetType() == type)
                RemoveComponent(_componentList[i]);
        }
    }

    public T GetComponent<T>() where T:Component {
        foreach (Component component in _componentList) {
            if (component.GetType() == typeof(T))
                return (T) component;
        }
        return null;
    }

    public List<Component> GetComponents(Type type) {
        List<Component> returnList = new List<Component>();
        foreach (Component component in _componentList) {
            if (component.GetType() == type)
                returnList.Add(component);
        }
        return returnList;
    }

    public void OnCollision(GameObject other) {
        foreach (Component component in _componentList) {
            component.OnCollision(other);
        }
    }
}
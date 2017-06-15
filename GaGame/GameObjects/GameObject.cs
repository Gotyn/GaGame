using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

public class GameObject : Object {
    private string name = "New GameObject";
    private Game _game;

    public Image image; //shouldnt be part of gameobject maybe.
    public Vec2 position = null;

    public List<Component> componentList = new List<Component>();


    public GameObject(Game pGame) {
        _game = pGame;
        _game.AddToGameObjects(this);
    }

    public GameObject(Game pGame, string pName) : this (pGame) {
        Name = pName;
    }

    public void Update() {
        //Console.WriteLine("Updateing {0}", name);
        //Update Components
        foreach (Component component in componentList) {
            component.Update();
        }
    }

    public Vec2 Center {
        get {
            return position + 0.5f * Size;
        }
    }
    public Vec2 Position {
        get {
            return position;
        }
    }
    public Vec2 Size {
        get {
            return new Vec2(image.Width, image.Height);
        }
    }

    public string Name { get => name; set => name = value; }
    public Game Game { get => _game; }

    public void AddComponent(Component component) {
        component.Owner = this;
        componentList.Add(component);
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

}
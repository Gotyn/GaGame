﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

public class GameObject : Object {
    private GameObject _parent;

    protected string _name = "New GameObject";
    public Image image; //shouldnt be part of gameobject maybe.
    public Vec2 position = null;

    public List<GameObject> childList = new List<GameObject>();
    public List<Component> componentList = new List<Component>();


    public GameObject(GameObject pParent = null) {
        if (pParent != null) Parent = pParent;
    }

    public GameObject(string pName, GameObject pParent = null) : this(pParent) {
        _name = pName;
    }

    virtual public void Update() {
        //Update Children
        foreach (GameObject child in childList) {
            child.Update();
        }

        //Update Components
        foreach (Component component in componentList) {
            component.Update();
        }
    }

    public GameObject Parent {
        get {
            return _parent;
        }
        set {
            _parent = value;
            _parent.childList.Add(this); //add self to list of children from parent.
        }
    }

    public void UpdateThroughChildren() {
        foreach (GameObject child in childList) {
            child.UpdateThroughChildren();
        }
        Update();
    }

    public void DrawThroughChildren(Graphics graphics) {
        foreach (GameObject child in childList) {
            child.DrawThroughChildren(graphics);
        }
        Draw(graphics);
    }

    public void ListChildren() {
        foreach (GameObject child in childList) {
            Console.WriteLine(child._name);
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

    public void Draw(Graphics graphics) {
        if (position != null) graphics.DrawImage(image, position.X, position.Y);
    }

    public void AddComponent(Component component) {
        componentList.Add(component);
        component.Owner = this;
    }

    public void RemoveComponent(Component component) {
        componentList.Remove(component);
        component.Owner = null;
    }

    public void UpdateComponents() {
        foreach (Component component in componentList) {
            component.Update();
        }
    }

    public List<GameObject> GetChildren {
        get { return childList; }
    }

    public List<Component> GetComponent {
        get { return componentList; }
    }

}
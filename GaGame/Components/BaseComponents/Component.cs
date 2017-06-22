abstract public class Component {
    private GameObject _owner;

    public Component() { }

    virtual public void Start() { }

    virtual public void Update() { }

    virtual public void OnCollision(GameObject other) { }

    public GameObject Owner { get => _owner; set => _owner = value; }
}
using System.Drawing;
using System.Diagnostics;

public class TextComponent : RenderComponent {
	private GameObject _paddle;
    private PaddleScript _paddleScript;

    public TextComponent() { }
    public TextComponent(Image image) : base(image) { }

    public override void Start() {
        base.Start();
        Debug.Assert(_paddle != null, "Paddle hasn't been set for " + Owner.Name);
        _paddleScript = _paddle.GetComponent<PaddleScript>();
        if (_paddleScript == null) {
            _paddleScript = _paddle.GetComponent<AutoPaddleScript>();
        }
    }

    public override void Draw(Graphics graphics, float timeIntoNextFrame = 0) {
        int digits = 2;
        string score = "000" + _paddleScript.Score.ToString();
        for (int d = 0; d < digits; d++) { // 3 digits left to right
            int digit = score[score.Length - digits + d] - 48; // '0' => 0 etc
            Rectangle rect = new Rectangle(digit * Image.Width / 10, 0, Image.Width / 10, Image.Height);
            graphics.DrawImage(Image, Owner.Position.X + d * Image.Width / 10, Owner.Position.Y, rect, GraphicsUnit.Pixel);
        }
    }

    public GameObject Paddle { get => _paddle; set => _paddle = value; }
}


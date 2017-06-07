/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class Paddle : GameObject
{
	//protected Image image;
	
	protected Ball ball = null;
	protected uint score;
	
	public const float Speed = 5.0f;

    protected InputComponent _inputComponent = new InputComponent();
    protected PhysicsComponent _physicsComponent = new PhysicsComponent();
    protected RenderComponent _renderComponent = new RenderComponent();


    public Paddle( string pName, float pX, float pY, string pImageFile, Ball pBall ) : base (pName, pX, pY, 0, 0, pImageFile)
	{
        image = Image.FromFile( pImageFile );
		velocity = new Vec2( 0, 0 );
		ball = pBall;
		score = 0;
    }

    override public void Update( Graphics graphics )
	{
        // input
        _inputComponent.Update(this);
        
        //move and collision & resolve
        _physicsComponent.Update(this, ball);
        				
		// render
        _renderComponent.Update(this, graphics);
    }

    public void IncScore() 
	{
		score++;
	}

    public bool Intersects(Vec2 otherPosition, Vec2 otherSize) {
        return
            this.position.X < otherPosition.X + otherSize.X && this.position.X + this.Size.X > otherPosition.X &&
            this.position.Y < otherPosition.Y + otherSize.Y && this.position.Y + this.Size.Y > otherPosition.Y;
    }

 //   public Vec2 Center {
	//	get {
	//		return position + 0.5f * Size;
	//	}
	//}	
	//public Vec2 Position {
	//	get { 
	//		return position;
	//	}
	//}
	//public Vec2 Size {
	//	get { 
	//		return new Vec2( image.Width, image.Height ); 
	//	}
	//}	

	public uint Score {
		get { 
			return score; 
		}
	}	
	

}



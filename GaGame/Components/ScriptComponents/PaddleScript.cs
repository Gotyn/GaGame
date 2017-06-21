﻿/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 2:01 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;


public class PaddleScript : Component
{
	protected uint score;

    private RigidBody _rigidBody;
    private GameObject _ball;
    private RenderComponent _ballRender;
	
    public override void Start() {
        score = 0;
        _rigidBody = Owner.GetComponent<RigidBody>();
        _ball = Locator.Game.FindGameObject("Ball");
        _ballRender = _ball.GetComponent<RenderComponent>();
        RestartEvent.Handlers += this.RestartHandler;
    }

    public override void Update() {
        // boundaries
        if (Owner.Position.Y < 0) Owner.Position.Y = 0;
        if (Owner.Position.Y > 416) Owner.Position.Y = 416;
    }

    public void IncScore() {
		score++;
	}
	
	public uint Score {
		get { 
			return score; 
		}
	}	

    void RestartHandler(object sender, RestartEvent e) {
        score = 0;
        Owner.Position.Y = 208;
    }
}



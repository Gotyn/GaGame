﻿/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 6:09 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;

public class ManualPaddle : Paddle {	
	public ManualPaddle(Game pGame, string pName, float pX, float pY, string pImageFile, Ball pBall) : base(pGame, pName, pX, pY, pImageFile, pBall ) {
        AddComponent(new tempManualPaddleComp());
    }

}
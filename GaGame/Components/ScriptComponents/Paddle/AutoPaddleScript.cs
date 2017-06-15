/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 6:09 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;

public class AutoPaddleScript : PaddleScript {
	public AutoPaddleScript(Game pGame, string pName, float pX, float pY, string pImageFile, GameObject pBall) : base(pGame, pName, pX, pY, pImageFile, pBall ) {
        Owner.AddComponent(new tempAutoPaddleComp());
    }

}


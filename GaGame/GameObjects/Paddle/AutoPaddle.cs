/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 6:09 PM
 */
using System;
using System.Drawing;
using System.Windows.Forms;

public class AutoPaddle : Paddle {
	public AutoPaddle( string pName, float pX, float pY, string pImageFile, Ball pBall, GameObject pParent = null) : base( pName, pX, pY, pImageFile, pBall, pParent ) { }
}


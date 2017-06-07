/*
 * Created by SharpDevelop.
 * User: Eelco Jannink
 * Date: 16-5-2017
 * Time: 18:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


static public class FrameCounter
{

	static int count = 0;
	
	static public void Update() 
	{
		if( Math.Floor( Time.Now ) > Math.Floor( Time.Now - Time.Step ) ) {
			Console.WriteLine("Fps : "+count );
			count = 0;
		} else {
			count++;
		}
	}
}


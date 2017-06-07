/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 4:36 PM
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

static public class Input
{
	
	static public void Init( Form  pForm ) {
		Key.Init( pForm );
	}
	
	static public class Key {
		
		static private Dictionary<Keys, float> keys = new Dictionary<Keys, float>();

		static public void Init( Form  pForm ) {
			pForm.KeyDown += new KeyEventHandler( Down );
			pForm.KeyUp += new KeyEventHandler( Up );
		}
		
	
		static private void Down( Object sender, KeyEventArgs e ) 
		{
			Keys key = e.KeyCode;
			float time = 0;
			keys.TryGetValue( key, out time );
			if( time == 0 ) { // non exists or upped
				keys[ key ] = Time.Real;
			}
		}
		
		static private void Up( Object sender, KeyEventArgs e ) 
		{
			keys[ e.KeyCode ] = 0;
		}
		
		static public bool Enter( Keys key ) 
		{
			float time = 0;
			if( keys.TryGetValue( key, out time ) ) {
				return time > Time.Now - Time.Step;
			}
			return false;
		}
		static public bool Pressed( Keys key ) 
		{
			float time = 0;
			if( keys.TryGetValue( key, out time ) ) {
				return time > 0;
			}
			return false;
		}
	
	}
}



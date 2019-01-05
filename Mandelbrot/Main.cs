using System;
using Gtk;
using Gdk;

namespace Mandelbrot
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MandelbrotWindow win = new MandelbrotWindow ();
			win.Show ();
			
			
			Application.Run ();
		}
	}
}
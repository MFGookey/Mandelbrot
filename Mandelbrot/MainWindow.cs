using System;
using Gtk;

public partial class MandelbrotWindow: Gtk.Window
{	
	public MandelbrotWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	/*protected void OnShowEvent(object sender, Shown a){
		Gdk.Drawable window = this.Canvas.GdkWindow;
		Gdk.GC gc = new Gdk.GC(window);
		gc.Foreground = new Gdk.Color(255, 0, 0);
		gc.Background = new Gdk.Color(255, 255, 255);
		window.DrawRectangle(gc, true, 50, 50, 20, 30);
	}*/
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	public Gtk.DrawingArea Canvas {
		get{return _canvas;}
	}
}
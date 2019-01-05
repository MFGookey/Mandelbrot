namespace GraphicBrot
{
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using System.Collections.Generic;

	public class BrotWindow : Form
	{

		private MandelbrotLib.Mandelbrot _plot;
		private double _xMin;
		private double _xMax;
		private double _yMin;
		private double _yMax;
		private bool _trackMouse;
		private int? _mouseStartX;
		private int? _mouseStartY;
		private Bitmap _buffer;
		private Rectangle? _mouseRect;
		private Stack<MandelbrotLib.Mandelbrot> _plotStack;
		private Stack<Bitmap> _graphicStack;
		
		public static void Main() {
			try{
			Application.Run(new BrotWindow());
			}
			catch(System.Exception e){
				return;
			}
		}

		public BrotWindow()
		{
			Text = "Mandelbrot Set";
			_xMin = -2;
			_xMax = 1.5;
			_yMin = -1;
			_yMax = 1;
			_plot = new MandelbrotLib.Mandelbrot(_xMin, _xMax, _yMin, _yMax, this.ClientSize.Width - 10, this.ClientSize.Height - 10);
			_plotStack = new Stack<MandelbrotLib.Mandelbrot>();
			_graphicStack = new Stack<Bitmap>();
			this.SetStyle(
			              ControlStyles.AllPaintingInWmPaint |
			              ControlStyles.UserPaint |
			              ControlStyles.DoubleBuffer,
			              true
			              );
			ResizeRedraw = true;
			_trackMouse = false;
			CenterToScreen();
		}
		
		protected override void OnSizeChanged (System.EventArgs e)
		{
			if(_buffer != null){
				_buffer.Dispose();
				_buffer = null;
			}
			
			//this should instead use the screen rectangle and points or something, now that we can zoom.
			_plot = new MandelbrotLib.Mandelbrot(_xMin, _xMax, _yMin, _yMax, this.ClientSize.Width - 10, this.ClientSize.Height - 10);
			_plot.GenerateField();
			base.OnSizeChanged (e);
		}
			                           
		protected override void OnPaint (System.Windows.Forms.PaintEventArgs e)
		{
			Pen pen = new Pen(Color.Black, 1);
			pen.DashStyle = DashStyle.Dot;
			Pen rectBack = new Pen(Color.White, 1);
			rectBack.DashStyle = DashStyle.Solid;
			
			if(_buffer == null){
				_buffer = new Bitmap(this.ClientSize.Width,this.ClientSize.Height);
			
				Graphics g = Graphics.FromImage(_buffer);
				
				for(int y=0;y<_plot.YCount;y++){
					for(int x=0;x<_plot.XCount;x++){
						if(_plot.PointIsInSet(y, x)){
							g.DrawLine(pen, x+5, y+5, x+6, y+5);
						}
					}
				}
				g.Dispose();
			}
			
			Graphics eventG = e.Graphics;
			eventG.DrawImageUnscaled(_buffer,0,0);
			
			if(_mouseRect != null){
				eventG.DrawRectangle(rectBack, _mouseRect.Value);
				eventG.DrawRectangle(pen, _mouseRect.Value);
			}
			eventG.Dispose();
		}
		
		protected override void OnPaintBackground(PaintEventArgs args){
			Brush backBrush = new SolidBrush(Color.White);
			args.Graphics.FillRectangle(backBrush, this.ClientRectangle);
		}
		
		protected override void OnMouseDown (System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown (e);
			if(_trackMouse == false && e.Button.Equals(MouseButtons.Left)){
				_trackMouse = true;
				_mouseStartX = e.X;
				_mouseStartY = e.Y;
			}
		}

		protected override void OnMouseMove (System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove (e);
			if(_trackMouse){
				int currentX = e.X;
				int currentY = e.Y;
				Point p = new Point(Math.Min(_mouseStartX.Value, currentX), Math.Min(_mouseStartY.Value, currentY));
				Size sz = new Size(Math.Max(_mouseStartX.Value, currentX) - p.X, Math.Max(_mouseStartY.Value, currentY) - p.Y);
				_mouseRect = new Rectangle(p, sz);
				this.Invalidate();
			}
		}
		
		protected override void OnMouseUp (System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp (e);
			if (_trackMouse){
				_trackMouse = false;
				_mouseStartX = null;
				_mouseStartY = null;
				if(_mouseRect.HasValue){
				MandelbrotLib.Complex upperLeft = _plot.GetPoint(_mouseRect.Value.Left, _mouseRect.Value.Top);
				MandelbrotLib.Complex lowerRight = _plot.GetPoint(_mouseRect.Value.Right, _mouseRect.Value.Bottom);
				_plotStack.Push(_plot);
				_graphicStack.Push(_buffer);
				_plot = new MandelbrotLib.Mandelbrot(upperLeft.Real, lowerRight.Real, lowerRight.Imaginary, upperLeft.Imaginary, this.ClientSize.Width, this.ClientSize.Height);
				_plot.GenerateField();
				_mouseRect = null;
				_buffer = null;
				this.Invalidate();
				}
			}
			else{
				if(e.Button.Equals(MouseButtons.Right)){
					if(_plotStack.Count > 0){
						_plot = _plotStack.Pop();
						_buffer = _graphicStack.Pop();
						this.Invalidate();
					}
				}
			}
		}
	}
}

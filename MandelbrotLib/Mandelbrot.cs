
using System;
using System.Collections.Generic;

namespace MandelbrotLib
{
	
	
	public class Mandelbrot
	{
		private MandelbrotPoint[,] _pointArray = new MandelbrotPoint[10,10];
		private double _xStep = 0.25;
		private double _yStep = 0.25;
		
		/// <summary>
		/// Center of the viewing area.
		/// </summary>
		private Complex _center = new Complex(0, 0);
		
		public int XCount {
			get{return _pointArray.GetLength(1);}
		}
		
		public int YCount {
			get{return _pointArray.GetLength(0);}
		}
		
		public Mandelbrot()
		{
			
		}
		
		public Mandelbrot(int width, int height, double xStep, double yStep, Complex center){
			_pointArray = new MandelbrotPoint[height,width];
			_xStep = xStep;
			_yStep = yStep;
			_center = new Complex(center);
			
		}
		
		public Mandelbrot(double xMin, double xMax, double yMin, double yMax, int xPoints, int yPoints){
			_pointArray = new MandelbrotPoint[yPoints, xPoints];
			_xStep = ((double)(xMax - xMin)) / ((double)xPoints);
			_yStep = ((double)(yMax - yMin)) / ((double)yPoints);
			_center = new Complex();
			_center.Real = ((double) xMax + xMin)/((double)2);
			_center.Imaginary = ((double) yMax + yMin)/((double)2);
		}
		
		public void GenerateField(){
			int centerY = _pointArray.GetLength(0)/2;
			int centerX = _pointArray.GetLength(1)/2;
			
			for(int y=0;y<_pointArray.GetLength(0);y++){
				for(int x=0;x<_pointArray.GetLength(1);x++){
					_pointArray[y,x] = new MandelbrotPoint(new Complex(_xStep*(x-centerX)+_center.Real, -_yStep*(y-centerY)+_center.Imaginary));
					_pointArray[y,x].TestPoint();
				}
			}
		}
		
		public bool PointIsInSet(int y, int x){
			return _pointArray[y, x].IsInSet;
		}
		
		public Complex GetPoint(int x, int y){
			return new Complex(_pointArray[y, x].Constant);
		}
	}
}

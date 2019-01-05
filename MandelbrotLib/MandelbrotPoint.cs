
using System;

namespace MandelbrotLib
{
	public class MandelbrotPoint
	{
		
		private Complex _constant;
		private Complex _currentPoint = new Complex();
		private bool _isInSet = true;
		private int _escapeVelocity = 0;
		private int _loopThreshold = 100;
		
		public Complex Constant{
			get {return _constant;}
			set {_constant = value;}
		}
		
		public Complex CurrentPoint{
			get {return _currentPoint;}
			set {_currentPoint = value;}
		}
		
		public bool IsInSet{
			get{return _isInSet;}
		}
		
		public int EscapeVelocity{
			get{return _escapeVelocity;}
		}
		
		public MandelbrotPoint(Complex constant)
		{
			_constant = constant;
		}
		
		public MandelbrotPoint(Complex constant, Complex currentPoint){
			_constant = constant;
			_currentPoint = currentPoint;
			this.TestPoint();
		}
		
		public void TestPoint(){
			if(_isInSet){
				double dist;
				//System.Console.Out.WriteLine(string.Format("Computing {0}", _constant.ToString()));
				for(int i=0;i < _loopThreshold && _isInSet;i++){
					_currentPoint *= _currentPoint;
					_currentPoint += _constant;
					dist = _currentPoint.getDistance();
					//System.Console.Out.WriteLine(string.Format("\t{0}\t{1}", _currentPoint.ToString(), dist));
					if(dist > 2){
						_isInSet = false;
						_escapeVelocity = i;
					}
				}
			}
		}
	}
}

using System;

namespace MandelbrotLib
{
	public class Complex
	{
		
		private double _real;
		private double _imaginary;
		
		public double Real{
			get{return _real;}
			set{_real = value;}
		}
		
		public double Imaginary{
			get{return _imaginary;}
			set{_imaginary = value;}
		}
		
		public Complex()
		{
		}
		
		public Complex(double real, double imaginary){
			_real = real;
			_imaginary = imaginary;
		}
		
		public Complex(Complex toCopy){
			_real = toCopy.Real;
			_imaginary = toCopy.Imaginary;
		}
		
		public double getDistance(){
			if(_real != 0 || _imaginary != 0){
				return Math.Sqrt(_real*_real+_imaginary*_imaginary);
			}
			else{
				return 0;
			}
		}
		
		public static Complex operator +(Complex left, Complex right) {
			return new Complex(left.Real+right.Real, left.Imaginary+right.Imaginary);
		}
		
		public static Complex operator *(Complex left, Complex right){
			return new Complex(left.Real * right.Real - left.Imaginary * right.Imaginary, left.Real * right.Imaginary + left.Imaginary * right.Real);
		}
		
		public override string ToString(){
			return string.Format("{0}{1}{2}i", this.Real, this.Imaginary >= 0? "+" : "", this.Imaginary);
		}
	}
}

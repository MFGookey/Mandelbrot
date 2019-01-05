using System;
using MandelbrotLib;

namespace ConsoleBrot
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Mandelbrot test = new Mandelbrot(-2.1, 0.5, -1.1, 1.1, 80, 44);
			test.GenerateField();
		}
	}
}
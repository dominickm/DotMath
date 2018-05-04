using System;
using System.Collections;
using DotMath.FixedPoint;

namespace DotMath
{
    public class DotMath
    {
        public static byte[] DoubleToFixedPoint(double d, int numBits, int binaryPoint)
        {
            return FixedPointConverter.DoubleToFixedPoint(d, numBits, binaryPoint);
        }
    }
}

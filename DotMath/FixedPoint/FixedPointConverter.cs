using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DotMath.FixedPoint
{
    class FixedPointConverter
    {
        public static byte[] DoubleToFixedPoint(double d, int numBits, int binaryPoint)
        {
            // This method converts a double to a fixed <numBits,decimalPlaces>
            BitArray bits = new BitArray(numBits);
            bits.SetAll(false);
            byte[] result = new byte[numBits / 8];
            bool isNegative = d < 0.0;
            Console.WriteLine("D: " + d.ToString());
            d = Math.Abs(d);
            double power = Math.Pow(2, numBits - binaryPoint - 1);
            for (int i = 0; i < numBits; i++)
            {
                if (d >= power)
                {
                    bits.Set(i, true);
                    d -= power;
                }
                power = power / 2.0;
            }
            if (isNegative)
            {
                bits.Not();
                int j = bits.Length - 1;
                bool process = true;
                while (process && j >= 0)
                {
                    if (bits.Get(j))
                    {
                        bits.Set(j, false);
                    }
                    else
                    {
                        bits.Set(j, true);
                        process = false;
                    }
                    j -= 1;
                }
            }

            string s = "";
            for (int i = 0; i < numBits; i++)
            {
                if (bits.Get(i))
                {
                    s += "1";
                }
                else
                {
                    s += "0";
                }
            }
            Console.WriteLine("BYTES: " + s);

            int index = 0;
            while (index * 8 < numBits)
            {
                for (int i = 0; i < 4; i++)
                {
                    bool bit = bits.Get(i + index * 8);
                    bits.Set(i + index * 8, bits.Get(7 - i + index * 8));
                    bits.Set(7 - i + index * 8, bit);
                }
                index += 1;
            }
            bits.CopyTo(result, 0);
            return result;
        }
    }
}

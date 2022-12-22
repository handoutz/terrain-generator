using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// A list of common math functions
    /// </summary>
    public static class Math
    {

        public static double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static double Lerp(double firstFloat, double secondFloat, double by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public static double Blerp(double c00, double c10, double c01, double c11, double tx, double ty)
        {
            return Lerp(Lerp(c00, c10, tx), Lerp(c01, c11, tx), ty);
        }

        public static double Normalize(double val, double max, double min) { return (val - min) / (max - min); }

        
    }

}


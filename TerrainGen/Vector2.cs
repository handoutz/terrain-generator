using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainGen
{
    internal class Vector2
    {
        internal double x, y;

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double SquareDistaceTo(Vector2 p)
            => (x - p.x) * (x - p.x) + (y - p.y) * (y - p.y);
        public double DistanceTo(Vector2 p)
            => SquareDistaceTo(p).Sqrt();
    }
    internal static class MathEx
    {
        public static double Sqrt(this double x)
            => Math.Sqrt(x);
        public static double Map(this double n, double start1, double stop1, double start2, double stop2)
            => ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
    }
}

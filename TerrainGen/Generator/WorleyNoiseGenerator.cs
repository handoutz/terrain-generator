using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainGen.Generator
{
    public class WorleyNoiseGenerator :IGenerator
    {
        private List<Vector2> _points;
        private int n;
        public void Apply(TerrainGrid grid)
        {
            var pointCt = 10;
            _points = new(pointCt);
            n = 5;
            var rando = new Random();
            for (int i = 0; i < pointCt; i++)
            {
                var x = rando.NextDouble().Map(0, 1, grid.Width, grid.Height);
                var y = rando.NextDouble().Map(0, 1, grid.Width, grid.Height);
                _points.Add(new Vector2(x,y));
            }
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var p = new Vector2(x, y);
                    var dist = _points.Select(pt => pt.SquareDistaceTo(p)).OrderBy(d => d).Take(n).Sum();
                    grid[x, y].Elevation = dist.Map(0, 1, 0, 1);
                }
            }
        }

        public string Name { get; }
        public double At(double x, double y)
            => At(new Vector2(x, y));
        double At(Vector2 p2)
            => _points.Select(p => p.SquareDistaceTo(p2)).OrderBy(x => x).ElementAt(n).Sqrt();
    }
}

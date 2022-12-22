using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noise;
using NoiseMixer;

namespace TerrainGen.Generator
{
    public class DebugGenerator :IGenerator
    {
        public List<Vector3> _points;
        public int n;
        public void Apply(TerrainGrid grid)
        {
            var nm = new Noise.NoiseMixer(grid.Width, grid.Height);
            nm.NewCombineLayer(new WorleyNoise(grid.Width, grid.Height, 14, 100), 2);
            
            var result = nm.Apply(false);
            //iterate grid
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    grid[x, y].Elevation = result[x, y];
                }
            }
        }

        public string Name { get; }
    }
}

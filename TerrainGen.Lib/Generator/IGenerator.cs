using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainGen.Generator
{
    public interface IGenerator
    {
        void Apply(TerrainGrid grid);
        string Name { get; }
    }
}

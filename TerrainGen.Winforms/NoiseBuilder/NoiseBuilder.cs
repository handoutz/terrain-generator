using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoiseMixer;
using TerrainGen.Generator;

namespace TerrainGen.NoiseBuilder
{
    public class NoiseBuilder
    {
        public enum MixType
        {
            Combine, Add, Divide, Multiply, OnlyHigher, OnlyLower, Subtract
        }
        private class Row
        {
            public INoise Noise { get; set; }
            public double Scale { get; set; }
            public MixType Mix { get; set; }
            public Action<Noise.NoiseMixer.LayerBase> ModifyLayer { get; set; }
        }
        private readonly uint _width;
        private readonly uint _height;
        public Noise.NoiseMixer NoiseMixer { get; }
        private List<Row> _rows = new();

        public NoiseBuilder(uint width, uint height)
        {
            _width = width;
            _height = height;
            NoiseMixer = new(width, height);
        }
        
        public void AddNoise(INoise noise, double scale, MixType mix,
            Action<Noise.NoiseMixer.LayerBase> modifyLayer)
        {
            _rows.Add(new Row() { Noise = noise, Scale = scale, Mix = mix, ModifyLayer = modifyLayer });
        }
        public void AddNoise(INoise noise, double scale, MixType mix)
        {
            _rows.Add(new Row()
            {
                Noise = noise,
                Scale = scale,
                Mix = mix,
                ModifyLayer = (ml) => { }
            });
        }

        public Noise.NoiseMixer Apply(TerrainGrid grid)
        {
            foreach (var row in _rows)
            {
                Noise.NoiseMixer.LayerBase layer = null;
                switch (row.Mix)
                {
                    case MixType.Combine:
                        layer = NoiseMixer.NewCombineLayer(row.Noise, row.Scale);
                        break;
                    case MixType.Add:
                        layer = NoiseMixer.NewAddLayer(row.Noise, row.Scale);
                        break;
                    case MixType.Divide:
                        layer = NoiseMixer.NewDivideLayer(row.Noise, row.Scale);
                        break;
                    case MixType.Multiply:
                        layer = NoiseMixer.NewMultiplyLayer(row.Noise, row.Scale);
                        break;
                    case MixType.OnlyHigher:
                        layer = NoiseMixer.NewOnlyHigherLayer(row.Noise, row.Scale);
                        break;
                    case MixType.OnlyLower:
                        layer = NoiseMixer.NewOnlyLowerLayer(row.Noise, row.Scale);
                        break;
                    case MixType.Subtract:
                        layer = NoiseMixer.NewSubtractLayer(row.Noise, row.Scale);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (row.ModifyLayer != null)
                    row.ModifyLayer(layer);
            }

            return NoiseMixer;
        }
    }
}

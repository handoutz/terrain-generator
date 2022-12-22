using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainGen.Generator
{
    public class GridSquare
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Elevation { get; set; }
        public override string ToString()
        {
            return $"({X},{Y}) ={Elevation}";
        }
    }
    public class TerrainGrid
    {
        public uint Width { get; set; }
        public uint Height { get; set; }
        public GridSquare[,] Grid { get; set; }
        public TerrainGrid(uint width, uint height)
        {
            Width = width;
            Height = height;
            Grid = new GridSquare[width, height];
            var rando = new Random();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x, y] = new GridSquare() { X = x, Y = y, Elevation = 0 };
                }
            }
        }
        //indexer for the grid
        public GridSquare this[int x, int y]
        {
            get
            {
                return Grid[x, y];
            }
            set
            {
                Grid[x, y] = value;
            }
        }
        //find the midpoint value of all points in this
        public double Midpoint()
        {
            double sum = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    sum += Grid[x, y].Elevation;
                }
            }
            return sum / (Width * Height);
        }
        //sample n amount of random points from this
        public List<GridSquare> SamplePoints(int n)
        {
            var rando = new Random();
            var points = new List<GridSquare>();
            for (int i = 0; i < n; i++)
            {
                points.Add(this[rando.Next(0, (int)Width), rando.Next(0, (int)Height)]);
                //points.Add(new Vector3(rando.NextDouble() * Width, rando.NextDouble() * Height, 0));
            }
            return points;
        }
    }
}

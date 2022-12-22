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
    }
    public class TerrainGrid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public GridSquare[,] Grid { get; set; }
        public TerrainGrid(int width, int height)
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
    }
}

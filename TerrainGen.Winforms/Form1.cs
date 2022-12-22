using TerrainGen.Generator;

namespace TerrainGen.Winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TerrainGrid Grid { get; set; } 
        private void Form1_Load(object sender, EventArgs e)
        {
            Grid = new TerrainGrid(512, 512);
            Width = (int)Grid.Width *3;
            Height = (int)Grid.Height;
            var noise = new WorleyNoiseGenerator();
            noise.Apply(Grid);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var mid = Grid.Midpoint();
            //iterate grid and draw on canvass
            //var img = new RenderTargetBitmap((int)Grid.Width, (int)Grid.Height, dpiX, dpiY, new PixelFormat());
            //var bitmap = new int[Grid.Width* Grid.Height];
            int width = (int)Grid.Width, height = (int)Grid.Height;
            var bm = new Bitmap(width, height);
            uint[] pixels = new uint[width * height];
            for (int y = 0; y < Grid.Height; y++)
            {
                int yIndex = (int)(y * Grid.Width);
                for (int x = 0; x < Grid.Width; x++)
                {
                    var square = Grid[x, y];
                    var ele = (double)square.Elevation;
                    var elevationColor = (ele * 255.0d);
                    int i = width * y + x;

                    int red = 0;
                    int green = 0;
                    int blue = 0;
                    int alpha = 255;
                    if (ele < mid) //water
                    {
                        blue = (int)elevationColor;
                        alpha = 255;
                        //bitmap[x+ yIndex] = Color.Blue.ToArgb();
                    }
                    else
                    {
                        green = (int)elevationColor;
                        //color blue based on how far from mid it is
                        //blue = (int)(255 - elevationColor);
                    }

                    bm.SetPixel(x, y, Color.FromArgb(alpha, red, green, blue));
                }
            }

            g.DrawImage(bm, new Point(0, 0));
            
            //retrieve 10 samples from Grid
            var samples = Grid.SamplePoints(10);
            var yText = 0;
            foreach (var sample in samples)
            {
                //var text = new FormattedText(sample.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, Brushes.Black);
                //drawingContext.DrawText(text, new Point(Grid.Width, yText));
                var txt = sample.ToString();
                var fnt = new Font(FontFamily.GenericSansSerif, 12);
                var measure = g.MeasureString(txt, fnt);
                g.DrawString(txt, fnt, Brushes.Black, new PointF(Grid.Width, yText));
                yText += (int)measure.Height;
            }
        }
    }
}
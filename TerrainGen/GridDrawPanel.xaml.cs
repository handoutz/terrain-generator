using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerrainGen.Generator;
using Color = System.Drawing.Color;
namespace TerrainGen
{
    /// <summary>
    /// Interaction logic for GridDrawPanel.xaml
    /// </summary>
    public partial class GridDrawPanel : UserControl
    {
        public GridDrawPanel()
        {
            InitializeComponent();
        }

        public GridDrawPanel(TerrainGrid grid) : this()
        {
            Grid = grid;
        }

        private TerrainGrid Grid { get; set; }
        private DrawingGroup _backStore { get; set; } = new();
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Render();
            drawingContext.DrawDrawing(_backStore);
        }

        private void Render()
        {
            var drawingContext = _backStore.Open();
            Render(drawingContext);
            drawingContext.Close();
        }

        private void Render(DrawingContext drawingContext)
        {
            var mid = Grid.Midpoint();
            //iterate grid and draw on canvas
            var source = PresentationSource.FromVisual(this);
            double dpiX = 96, dpiY = 96;
            if (source != null)
            {
                dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
                dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;
            }

            var image = new Image();
            //var img = new RenderTargetBitmap((int)Grid.Width, (int)Grid.Height, dpiX, dpiY, new PixelFormat());
            //var bitmap = new int[Grid.Width* Grid.Height];
            int width = (int)Grid.Width, height = (int)Grid.Height;
            WriteableBitmap bitmap = new WriteableBitmap(
                (int)Grid.Width, (int)Grid.Height, 96, 96, PixelFormats.Bgra32, null);
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

                    int red=0;
                    int green=0;
                    int blue=0;
                    int alpha=255;
                    if (ele < mid) //water
                    {
                        blue = 255;
                        alpha = 255;
                        //bitmap[x+ yIndex] = Color.Blue.ToArgb();
                    }
                    else
                    {
                        green = (int)elevationColor;
                    }
                    pixels[i] = (uint)((blue << 24) + (green << 16) + (red << 8) + alpha);
                }
            }

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, 0);
            drawingContext.DrawImage(bitmap, new Rect(0, 0, width, height));
            //retrieve 10 samples from Grid
            var samples = Grid.SamplePoints(10);
            var yText = 0;
            foreach (var sample in samples)
            {
                var text = new FormattedText(sample.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, Brushes.Black);
                drawingContext.DrawText(text, new Point(Grid.Width, yText));
                yText += (int)Math.Ceiling(text.Height);
            }
        }
    }
}

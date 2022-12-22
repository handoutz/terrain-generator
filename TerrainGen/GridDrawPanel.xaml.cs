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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerrainGen.Generator;

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
            //iterate grid and draw on canvas
            for (int x = 0; x < Grid.Width; x++)
            {
                for (int y = 0; y < Grid.Height; y++)
                {
                    var square = Grid[x, y];
                    var brush = new SolidColorBrush(Color.FromRgb((byte)(square.Elevation * 255), (byte)(square.Elevation * 255), (byte)(square.Elevation * 255)));
                    drawingContext.DrawRectangle(brush, null, new Rect(x, y, 1, 1));
                }
            }
        }
    }
}

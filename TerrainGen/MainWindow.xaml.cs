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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private TerrainGrid Grid { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            Grid = new TerrainGrid(256, 256); 
            var noise = new WorleyNoiseGenerator();
            noise.Apply(Grid);
            grid.Children.Add(new GridDrawPanel(Grid));
            base.OnInitialized(e);
        }

    }
}

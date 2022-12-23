using NoiseMixer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerrainGen.Winforms.NoiseBuilder
{
    public partial class LayerManager : UserControl
    {
        public LayerManager()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var picker = new NoisePicker();
            if (picker.ShowDialog() == DialogResult.Yes)
            {
                var theType = picker.SelectedNoise;
                
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }

        private void LayerManager_Load(object sender, EventArgs e)
        {
            
        }
    }
}

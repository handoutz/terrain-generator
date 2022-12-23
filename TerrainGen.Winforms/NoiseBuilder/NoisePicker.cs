using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoiseMixer;

namespace TerrainGen.Winforms.NoiseBuilder
{
    public partial class NoisePicker : Form
    {
        private class ParamInfo
        {
            public ParameterInfo ReflectionInfo { get; set; }
            public string Name { get; set; }
            public int Order { get; set; }
        }
        public NoisePicker()
        {
            InitializeComponent();
        }

        public Type SelectedNoise { get; set; }
        private Dictionary<string, Type> _noise = new();
        private void NoisePicker_Load(object sender, EventArgs e)
        {
            foreach(var noise in NoiseManager.GetNoiseTypes())
            {
                _noise.Add(noise.Name, noise);
                listBox1.Items.Add(noise.Name);
            }
        }
        
        private List<ParamInfo> _curParamTextBoxes = new();
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _curParamTextBoxes.Clear();
            tlpParams.Controls.Clear();
            var typeParams = NoiseManager.GetNoiseConstructorParams(_noise[listBox1.SelectedItem.ToString()]);
            tlpParams.ColumnCount = 2;
            tlpParams.RowCount = typeParams.Count;
            tlpParams.RowStyles.Clear();
            tlpParams.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            for (var index = 0; index < typeParams.Count; index++)
            {
                var param = typeParams[index];
                var label = new Label();
                label.Text = $"{param.Name}: {param.ParameterType.Name}";
                tlpParams.Controls.Add(label);
                var tb = new TextBox();
                tlpParams.Controls.Add(tb);
                _curParamTextBoxes.Add(new()
                {
                    Name = param.Name,
                    Order = index,
                    ReflectionInfo = param
                });
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace SampleCode
{
    /// <summary>
    /// Interaction logic for layerPallette.xaml
    /// </summary>
    public partial class layerPallette : Window
    {
        public layerPallette(ref List<String> layers)
        {
            InitializeComponent();
            UpdateLayers(layers);
        }

        public void UpdateLayers(List<String> layers)
        {
            layerList.Items.Clear();
            for (int i = 0; i < layers.Count(); i++)
            {
                layerList.Items.Add(layers[i]);
            }
        }

        public void changeSelection(int index)
        {
            layerList.SelectedIndex = index;
        }
    }
}

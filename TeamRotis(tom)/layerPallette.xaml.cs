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
        public layerPallette(ObservableCollection<RectangleViewModel> layers)
        {
            InitializeComponent();
            UpdateLayers(layers);
        }

        public void UpdateLayers(ObservableCollection<RectangleViewModel> layers)
        {
            layerList.Items.Clear();
            for (int i = layers.Count() - 1; i >= 0; --i)
            {
                layerList.Items.Add(layers[i].RectName);
            }
        }

        protected override void  OnClosing(System.ComponentModel.CancelEventArgs e)
        {
 	        e.Cancel = true;
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}

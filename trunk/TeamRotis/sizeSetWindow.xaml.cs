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

namespace SampleCode
{
    /// <summary>
    /// Interaction logic for sizeSetWindow.xaml
    /// </summary>
    public partial class sizeSetWindow : Window
    {
        public String result;
        public sizeSetWindow(ref String parentResult)
        {
            result = parentResult;
            InitializeComponent();
        }

        private void okayBut_Click(object sender, RoutedEventArgs e)
        {
            if (widthText.Text == "")
            {
                MessageBox.Show("Please enter a width.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (heightText.Text == "")
            {
                MessageBox.Show("Please enter a height.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                result = widthText.Text + "x" + heightText.Text;
                this.Close();
            }
        }

        private void cancelBut_Click(object sender, RoutedEventArgs e)
        {
            result = "Cancel";
            this.Close();
        }


    }
}

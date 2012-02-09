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
    /// Interaction logic for newImageDialog.xaml
    /// </summary>
    public partial class newImageDialog : Window
    {
        MainWindow firstformRef;
        public newImageDialog(ref MainWindow form1)
        {
            firstformRef = form1;
            //InitializeComponent();
        }

        private void okayBut_Click(object sender, RoutedEventArgs e)
        {
            //exit(heightText.Text + "x" + widthText.Text);
        }

        private String exit(String dims)
        {
            this.Close();
            return dims;
        }

        private void cancelBut_Click(object sender, RoutedEventArgs e)
        {
            
            exit("Cancel");
        }
    }
}

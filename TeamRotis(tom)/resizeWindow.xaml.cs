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
    /// Interaction logic for resizeWindow.xaml
    /// </summary>
    public partial class resizeWindow : Window
    {
        public String result;
        public String myType;
        public resizeWindow(ref String parentResult, String type)
        {
            result = parentResult;
            InitializeComponent();
            myType = type;
            label1.Content = myType;
            resizeText.Focus();
        }

        private void okBut_Click(object sender, RoutedEventArgs e)
        {
            if (resizeText.Text == "")
            {
                MessageBox.Show("Please enter a value.", label1.Content + " Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                result = resizeText.Text;
                this.Close();
            }
        }

        private void cancelBut_Click(object sender, RoutedEventArgs e)
        {
            result = "Cancel";
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                okBut_Click(sender, e);
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                cancelBut_Click(sender, e);
                e.Handled = true;
            }
        }

    }
}

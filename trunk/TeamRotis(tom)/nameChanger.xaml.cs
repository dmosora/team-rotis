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
    /// Interaction logic for nameChanger.xaml
    /// </summary>
    public partial class nameChanger : Window
    {
        public string result;
        public nameChanger(ref string parentResult)
        {
            result = parentResult;
            InitializeComponent();
            nameText.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                okBut_Click(sender, e);
                e.Handled = true;
            }
        }

        private void okBut_Click(object sender, RoutedEventArgs e)
        {
            if (nameText.Text == "")
            {
                MessageBox.Show("Please enter a name.", "Invalid Name", MessageBoxButton.OK);
            }
            else
            {
                result = nameText.Text;
                this.Close();
            }
        }

        private void cancelBut_Click(object sender, RoutedEventArgs e)
        {
            result = "";
            this.Close();
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {

        }
    }
}

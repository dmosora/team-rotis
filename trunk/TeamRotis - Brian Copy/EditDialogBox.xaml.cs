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
using System.ComponentModel;

namespace SampleCode
{
    /// <summary>
    /// Interaction logic for EditDialogBox.xaml
    /// </summary>
    public partial class EditDialogBox : Window
    {
        public EditDialogBox()
        {
            InitializeComponent();
            opacityTextBox.Focus();
          
        }

        void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Dialog box canceled
            this.DialogResult = false;
        }

        void okButton_Click(object sender, RoutedEventArgs e)
        {
          // Do data Validation

            // Dialog box accepted
            this.DialogResult = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                okButton_Click(sender, e);
                e.Handled = true;
            }
        }


    }
}

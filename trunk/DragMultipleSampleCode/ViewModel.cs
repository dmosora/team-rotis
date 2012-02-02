using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SampleCode
{
    /// <summary>
    /// A simple example of a view-model.  
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        #region Data Members

        /// <summary>
        /// The list of rectangles that is displayed in the ListBox.
        /// </summary>
        private ObservableCollection<RectangleViewModel> rectangles = new ObservableCollection<RectangleViewModel>();

        #endregion Data Members

        public ViewModel()
        {

        }

        public void addRectangle(string imageuri)
        {
            //Message Box for Opacity
            string messageBoxText = "Do you want to make this image opaque?";
            string caption = "Image Opacity";
            MessageBoxButton button = MessageBoxButton.YesNo;
            // Display message box
            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);

            double opac = 0;
            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                        opac = .5;
                    break;
                case MessageBoxResult.No:
                        opac=1;
                    break;
            }

            
            BitmapImage bitemp = new BitmapImage();
            bitemp.BeginInit();
            bitemp.UriSource = new Uri(imageuri, UriKind.Relative);
            bitemp.EndInit();
            var rtemp = new RectangleViewModel(150, 130,bitemp.PixelWidth, bitemp.PixelHeight, bitemp, opac);
            rectangles.Add(rtemp);

           
        }

        public void deleteRectangle(RectangleViewModel sender)
        {
            rectangles.RemoveAt(rectangles.IndexOf(sender));
        }

        /// <summary>
        /// The list of rectangles that is displayed in the ListBox.
        /// </summary>
        public ObservableCollection<RectangleViewModel> Rectangles
        {
            get
            {
                return rectangles;
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the 'PropertyChanged' event when the value of a property of the view model has changed.
        /// </summary>
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// 'PropertyChanged' event that is raised when the value of a property of the view model has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

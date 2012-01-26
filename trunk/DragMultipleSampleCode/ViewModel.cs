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
            //
            // Populate the view model with some example data.
            //
           BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Koala.jpg", UriKind.Relative);
            bi3.EndInit();
            BitmapImage bi4 = new BitmapImage();
            bi4.BeginInit();
            bi4.UriSource = new Uri("Koala.jpg", UriKind.Relative);
            bi4.EndInit();

            var r1 = new RectangleViewModel(10, 10, 50, 40, bi3);
            rectangles.Add(r1);
            var r2 = new RectangleViewModel(70, 60, 50, 60, bi4);
            rectangles.Add(r2);
            //var r3 = new RectangleViewModel(150, 130, 55, 48, new Uri("smiley_stackpanel.PNG", UriKind.Relative));
            //rectangles.Add(r3);
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

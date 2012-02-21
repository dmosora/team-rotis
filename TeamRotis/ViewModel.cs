using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

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

        // saved date for undo and redo
        private List<ObservableCollection<RectangleViewModel>> savedstates = new List<ObservableCollection<RectangleViewModel>>();
        int current=-1;

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


            // Create a memorystream with the image URI then loads that to the rectangle veiw model using a Bitmap Image Source
            BitmapImage bitemp = new BitmapImage();

            if (File.Exists(imageuri))
            {
                MemoryStream memoryStream = new MemoryStream();

                byte[] fileBytes = File.ReadAllBytes(imageuri);
                memoryStream.Write(fileBytes, 0, fileBytes.Length);
                memoryStream.Position = 0;

                bitemp.BeginInit();
                bitemp.StreamSource = memoryStream;
                bitemp.EndInit();
            }          
            
            var rtemp = new RectangleViewModel(150, 130,bitemp.PixelWidth, bitemp.PixelHeight, bitemp, opac);
            rectangles.Add(rtemp);
            bitemp.UriSource = new Uri("", UriKind.Relative);

           
        }

        public void saveRectangle(string imageuri)
        {

        }

        public void deleteRectangle(RectangleViewModel sender)
        {
            rectangles.RemoveAt(rectangles.IndexOf(sender));
        }

        public void makeTopLayer(RectangleViewModel sender)
        {
            int start = rectangles.IndexOf(sender);
            int i = 0;
            if (start >= 0)
            {
                for (i = start; i < rectangles.Count - 1; i++)
                {
                    rectangles[i] = rectangles[i + 1];
                }
            }
            rectangles[i] = sender;
        }

        public double getWidth(double start)
        {
            double left = start;
            double right = 0;
            for (int i = 0; i < rectangles.Count; i++)
            {
                RectangleViewModel temp = rectangles[i];
                left = Math.Min(temp.X, left);
            }
            for (int i = 0; i < rectangles.Count; i++)
            {
                RectangleViewModel temp = rectangles[i];
                right = Math.Max((temp.X+temp.Width), right);
            }
            return right-left;
        }
        public double getHeight(double start)
        {
            double top = start;
            double bottum = 0;
            for (int i = 0; i < rectangles.Count; i++)
            {
                RectangleViewModel temp = rectangles[i];
                top = Math.Min(temp.Y, top);
            }
            for (int i = 0; i < rectangles.Count; i++)
            {
                RectangleViewModel temp = rectangles[i];
                bottum = Math.Max((temp.Y + temp.Height), bottum);
            }
            return bottum-top;
        }
        public double getLeft(double start)
        {
            double left = start;

            for (int i = 0; i < rectangles.Count; i++)
            {
                RectangleViewModel temp = rectangles[i];
                left = Math.Min(temp.X, left);
            }
            return left;
        }
        public double getTop(double start)
        {
            double top = start;
            for (int i = 0; i < rectangles.Count; i++)
            {
                RectangleViewModel temp = rectangles[i];
                top = Math.Min(temp.Y, top);
            }
            return top;
        }

        public void saveState()
        {
            current++;
            ObservableCollection<RectangleViewModel> temp = new ObservableCollection<RectangleViewModel>();
            foreach(RectangleViewModel tosave in rectangles)
            {
                RectangleViewModel tempRect = new RectangleViewModel(tosave);
                temp.Insert(rectangles.IndexOf(tosave), tempRect);
            }
            if (current < savedstates.Count())
            {
                savedstates[current] = temp;
            }
            else
            {
                savedstates.Add(temp);
            }
        }

        public void undo()
        {
            if (current > 0)
            {
                current--;
                rectangles.Clear();
                foreach (RectangleViewModel tosave in savedstates[current])
                {
                    rectangles.Insert(savedstates[current].IndexOf(tosave), tosave);
                }
            }
        }

        public void redo()
        {
            if (current < (savedstates.Count()-1))
            {
                current++;
                rectangles.Clear();
                foreach (RectangleViewModel tosave in savedstates[current])
                {
                    rectangles.Insert(savedstates[current].IndexOf(tosave), tosave);
                }
            }
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

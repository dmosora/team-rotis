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
using System.Globalization;

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
        int top = -1;

        // saved data for copy paste
        private RectangleViewModel[] copied;

        #endregion Data Members

        public ViewModel()
        {

        }

        public RectangleViewModel addRectangle(string imageuri)
        {

            double opac = 1;

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

            var rtemp = new RectangleViewModel(150, 130, bitemp.PixelWidth, bitemp.PixelHeight, bitemp, opac, "Layer " + (rectangles.Count() + 1).ToString());
            rectangles.Add(rtemp);
            bitemp.UriSource = new Uri("", UriKind.Relative);

            return rtemp;
           
        }

        public RectangleViewModel addRectangle(double width, double height)
        {
            double opac = 1;
            // Create a memorystream with the image URI then loads that to the rectangle veiw model using a Bitmap Image Source
            BitmapImage bitemp = new BitmapImage();
            int myWidth = (Int32)width;
            int myHeight = (Int32)height;
            int stride = myWidth * 4;
            BitmapSource i = BitmapImage.Create(
            myWidth,
            myHeight,
            96,
            96,
            PixelFormats.Indexed1,
            new BitmapPalette(new List<Color> { Colors.Transparent }),
            new byte[stride * myHeight],
            stride);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream mem = new MemoryStream();

            encoder.Frames.Add(BitmapFrame.Create(i));
            encoder.Save(mem);



                /*MemoryStream memoryStream = new MemoryStream();

                byte[] fileBytes = File.ReadAllBytes("/" + this.GetType().Assembly.GetName().Name + ";component/resources/transparent.png");
                memoryStream.Write(fileBytes, 0, fileBytes.Length);
                memoryStream.Position = 0;

                bitemp.BeginInit();
                bitemp.StreamSource = memoryStream;
                bitemp.EndInit();*/

            bitemp.BeginInit();
            bitemp.StreamSource = new MemoryStream(mem.ToArray());
            bitemp.EndInit();

            var rtemp = new RectangleViewModel(150, 130, bitemp.PixelWidth, bitemp.PixelHeight, bitemp, opac, "Layer " + (rectangles.Count() + 1).ToString());
            rectangles.Add(rtemp);
            bitemp.UriSource = new Uri("", UriKind.Relative);

            return rtemp;
        }

        public RectangleViewModel addRectangle(double x, double y,double width, double height)
        {
            double opac = 1;
            // Create a memorystream with the image URI then loads that to the rectangle veiw model using a Bitmap Image Source
            BitmapImage bitemp = new BitmapImage();

            int myWidth = (Int32)width;
            int myHeight = (Int32)height;
            int stride = myWidth * 4;
            BitmapSource i = BitmapImage.Create(
            myWidth,
            myHeight,
            96,
            96,
            PixelFormats.Indexed1,
            new BitmapPalette(new List<Color> { Colors.Transparent }),
            new byte[stride * myHeight],
            stride);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream mem = new MemoryStream();

            encoder.Frames.Add(BitmapFrame.Create(i));
            encoder.Save(mem);

            bitemp.BeginInit();
            bitemp.StreamSource = new MemoryStream(mem.ToArray());
            bitemp.EndInit();

            var rtemp = new RectangleViewModel(x, y, bitemp.PixelWidth, bitemp.PixelHeight, bitemp, opac, "Layer " + (rectangles.Count() + 1).ToString());
            rectangles.Add(rtemp);
            bitemp.UriSource = new Uri("", UriKind.Relative);

            return rtemp;
        }

        public void copy(RectangleViewModel[] sender)
        {
            double leftbound = getLeft(sender, 10000);
            double topbound = getTop(sender, 10000);
            copied = new RectangleViewModel[sender.Length];
            int j = 0;

            foreach (RectangleViewModel RVM in sender)
            {
                if (RVM != null)
                {
                    // save each rectangle into copied
                    copied[j] = new RectangleViewModel(RVM.X - leftbound, RVM.Y - topbound, RVM.Width, RVM.Height, RVM.BImage, RVM.Opacity, RVM.RectName);
                    copied[j].RAngle = RVM.RAngle;
                    copied[j].Scale = RVM.Scale;
                    j++;
                }
            }
        }

        public void paste(Point mousePosition)
        {
            for (int j = 0; j < copied.Length; j++)
            {
                if (copied[j] != null)
                {
                    //Add each ectangle, with upper left corner where the mouse clicked paste
                    var rtemp = new RectangleViewModel(copied[j].X + mousePosition.X, copied[j].Y + mousePosition.Y - 25, copied[j].Width, copied[j].Height, copied[j].BImage, copied[j].Opacity, copied[j].RectName);
                    rtemp.RAngle = copied[j].RAngle;
                    rtemp.Scale = copied[j].Scale;
                    rectangles.Add(rtemp);
                }
            }
        }

        public double getLeft(RectangleViewModel[] sender, double start)
        {
            double left = start;

            foreach (RectangleViewModel RVM in sender)
            {
                if (RVM != null)
                {
                    RectangleViewModel temp = RVM;
                    left = Math.Min(temp.X, left);
                }
            }
            return left;
        }
        public double getTop(RectangleViewModel[] sender, double start)
        {
            double top = start;
            foreach (RectangleViewModel RVM in sender)
            {
                if (RVM != null)
                {
                    RectangleViewModel temp = RVM;
                    top = Math.Min(temp.Y, top);
                }
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
            top = current;
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
            if (current < top)
            {
                current++;
                rectangles.Clear();
                foreach (RectangleViewModel tosave in savedstates[current])
                {
                    rectangles.Insert(savedstates[current].IndexOf(tosave), tosave);
                }
            }
        }

        public MemoryStream RectToStream(RectangleViewModel sender)
        {
            
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(sender.BImage));
                enc.Save(outStream);
                return outStream;
            }

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

        public void editRectangle(RectangleViewModel sender)
        {
            //this will update on a filter

            //Message Box for Opacity

            EditDialogBox dlg = new EditDialogBox();
            // Open the dialog box modally 
            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {
                // Opacity Setting after update menu
                sender.Opacity = Convert.ToDouble(dlg.opacityTextBox.Text);
                sender.RAngle = Convert.ToDouble(dlg.rotateTextBox.Text);
                sender.Scale = Convert.ToDouble(dlg.scaleTextBox.Text);
            }
            else
            {

            }
        }
        public void effectEmboss(RectangleViewModel sender)
        {
            //this will update on a filter
            //Message Box for Opacity

            // Create source.
            double[, ,] MCP = new double[3, 3, 1] {{ {-1} , {0}, {-1} }, 
                                                    { {0} , {4}, {0} }, 
                                                    { {-1} , {0}, {-1} } };
            double _factor = MCP[1, 1, 0] + 8;
            double _offset = 127;
            sender.imageConvolution(MCP, _factor, _offset);


        }
        public void effectGaussianBlur(RectangleViewModel sender)
        {
            //this will update on a filter
            //Message Box for Opacity

            // Create source.
            double[, ,] MCP = new double[3, 3, 1] {{ {1} , {2}, {1} }, 
                                                    { {2} , {4}, {2} }, 
                                                    { {1} , {2}, {1} } };
            double _factor = 16;
            double _offset = 0;
            sender.imageConvolution(MCP, _factor, _offset);


        }
        public void effectSmoothing(RectangleViewModel sender)
        {
            //this will update on a filter
            //Message Box for Opacity

            // Create source.
            double[, ,] MCP = new double[3, 3, 1] {{ {1} , {1}, {1} }, 
                                                    { {1} , {8}, {1} }, 
                                                    { {1} , {1}, {1} } };
            double _factor = 16;
            double _offset = 0;
            sender.imageConvolution(MCP, _factor, _offset);


        }
        public void effectEdgeDetection(RectangleViewModel sender)
        {
            //this will update on a filter
            //Message Box for Opacity

            // Create source.
            double[, ,] MCP = new double[3, 3, 1] {{ {0} , {-1}, {0} }, 
                                                    { {-1} , {4}, {-1} }, 
                                                    { {0} , {-1}, {0} } };
            double[, ,] MCP2 = new double[3, 3, 1] {{ {1} , {1}, {1} }, 
                                                    { {1} , {8}, {1} }, 
                                                    { {1} , {1}, {1} } };
            double _factor2 = 16;
            double _offset2 = 0;
            double _factor = 1;
            double _offset = -25;
            sender.imageConvolution(MCP2, _factor2, _offset2);
            sender.imageConvolution(MCP, _factor, _offset);


        }

        public void effectGrayscale(RectangleViewModel sender)
        {
            //this will update on a filter
            //Message Box for Opacity
            double[, ,] MCP = new double[3, 3, 1] {{ {0} , {0}, {0} }, 
                                                    { {0} , {1}, {0} }, 
                                                    { {0} , {0}, {0} } };
            // Create source.

            double red = .3;
            double blue = .11;
            double green = .59;
            sender.colorConvolution(MCP, red, blue, green);


        }

        public void imageSelection(int __topLeftX, int __topLeftY, int __width, int __height, RectangleViewModel sender)
        {
            //  (_topLeftX, __topLeftY, _width, _height, selected[i]);

            sender.imageSelectionDeletion(__topLeftX, __topLeftY, __width, __height);


        }
        public String imageSelectionCopy(int __topLeftX, int __topLeftY, int __width, int __height, RectangleViewModel sender)
        {
            //  (_topLeftX, __topLeftY, _width, _height, selected[i]);
            string newSelection = sender.imageSelectionRetrivial(__topLeftX, __topLeftY, __width, __height);

            return newSelection;
        }
        public void imageSelectionRotated(Point __top, Point __right, Point __bottum, Point __left, RectangleViewModel sender)
        {

            //imageSelectionRotated(top, right, bottum, left, selected[i]);
            sender.imageSelectionDeletionRotated(__top, __right, __bottum, __left);
            //string newSelection = sender.imageSelectionDeletionRotated(__top, __right, __bottum, __left);

            //return newSelection;
            
        }
        public String imageSelectionCopyRotated(Point __top, Point __right, Point __bottum, Point __left, RectangleViewModel sender)
        {

            //imageSelectionRotated(top, right, bottum, left, selected[i]);
            string newSelection = sender.imageSelectionRetrivialRotated(__top, __right, __bottum, __left);
            //string newSelection = sender.imageSelectionDeletionRotated(__top, __right, __bottum, __left);

            //return newSelection;
            return newSelection;
        }

        public void addCircle(RectangleViewModel sender, Point center, Color fillColor)
        {
            // Create the ellipse geometry to add to the Path
            GeometryDrawing circleHolder =
                new GeometryDrawing(
                    new SolidColorBrush(fillColor),
                    new Pen(Brushes.Green, 0),
                    new EllipseGeometry(center, 3, 3)
                );
            
           sender.Draw.Children.Add(circleHolder);

         }

        public void addLine(RectangleViewModel sender, Point start, Point end, Color fillColor)
        {
            // Create the line geometry to add to the Path
            GeometryDrawing lineHolder =
                new GeometryDrawing(
                    new SolidColorBrush(fillColor),
                    new Pen(new SolidColorBrush(fillColor), 6),
                    new LineGeometry(start, end)
                );

            sender.Draw.Children.Add(lineHolder);
        }

        public void addString(RectangleViewModel sender, Point start, string stuff, Color fillColor)
        {
            FormattedText text = new FormattedText(stuff,
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            new Typeface("Tahoma"),
            24,
            Brushes.Black);
            Geometry tgeometry = text.BuildGeometry(start);
             GeometryDrawing textHolder =
                new GeometryDrawing(new SolidColorBrush(fillColor),
                    new Pen(Brushes.Black, 1),
                    tgeometry
                );
            sender.Draw.Children.Add(textHolder);
        }

        public void addOval(RectangleViewModel sender, Point center, double height, double width, Color fillColor)
        {
            // Create the ellipse geometry to add to the Path
            GeometryDrawing circleHolder =
                new GeometryDrawing(
                    new SolidColorBrush(fillColor),
                    new Pen(Brushes.Green, 0),
                    new EllipseGeometry(center, height, width)
                );

            sender.Draw.Children.Add(circleHolder);
        }

        public void addRect(RectangleViewModel sender, Point center, double height, double width, Color fillColor)
        {
            // Create the ellipse geometry to add to the Path
            Rect toDraw = new Rect(0,0, height, width);
            GeometryDrawing rectangleHolder =
                new GeometryDrawing(
                    new SolidColorBrush(fillColor),
                    new Pen(Brushes.Green, 0),
                    new RectangleGeometry(toDraw)
                );

            sender.Draw.Children.Add(rectangleHolder);
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

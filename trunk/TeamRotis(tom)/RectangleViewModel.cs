using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;





namespace SampleCode
{

    public class pixelHolder : INotifyPropertyChanged
    {
        #region Data Members
        byte blue;
        byte red;
        byte green;
        byte alpha;
        #endregion Data Members
        public pixelHolder()
        {
        }
        public pixelHolder(byte _blue, byte _green, byte _red, byte _alpha)
        {
            this.blue = _blue;
            this.red = _red;
            this.green = _green;
            this.alpha = _alpha;
        }
        #region INotifyPropertyChanged Members

        public byte Blue
        {
            get
            {
                return blue;
            }
            set
            {
                if (blue == value)
                {
                    return;
                }

                blue = value;

                OnPropertyChanged("Blue");
            }
        }

        public byte Red
        {
            get
            {
                return red;
            }
            set
            {
                if (red == value)
                {
                    return;
                }

                red = value;

                OnPropertyChanged("Red");
            }
        }

        public byte Green
        {
            get
            {
                return green;
            }
            set
            {
                if (green == value)
                {
                    return;
                }

                green = value;

                OnPropertyChanged("Green");
            }
        }

        public byte Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                if (alpha == value)
                {
                    return;
                }

                alpha = value;

                OnPropertyChanged("Alpha");
            }
        }
        /// <summary>
        /// Raises the 'PropertyChanged' event when the value of a property of the view model has changed.
        /// </summary>
        protected void OnPropertyChanged(string name)
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
    






    
    /// <summary>
    /// Defines the view-model for a simple displayable rectangle.
    /// </summary>
    public class RectangleViewModel : INotifyPropertyChanged
    {
        #region Data Members

        /// <summary>
        /// The X coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        private double x = 0;

        /// <summary>
        /// The Y coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        private double y = 0;

        /// <summary>
        /// The width of the rectangle (in content coordinates).
        /// </summary>
        private double width = 0;

        /// <summary>
        /// The height of the rectangle (in content coordinates).
        /// </summary>
        private double height = 0;

        /// <summary>
        /// The color of the rectangle.
        /// </summary>
        private BitmapImage bImage;
        private ImageSource iSource;
        private double rAngle = 0;
        private double scale = 1;
        private double opacity = 1;

        // The drawing canvas for the rectangle.
        private DrawingGroup draw;

        /// <summary>
        /// The hotspot of the rectangle's connector.
        /// This value is pushed through from the UI because it is data-bound to 'Hotspot'
        /// in ConnectorItem.
        /// </summary>
        private Point connectorHotspot;

        /// <summary>
        /// Used to define a name for the given rectangle. Defaults to the position in the view model.
        /// </summary>
        private string rectName;

        #endregion Data Members

        

        public RectangleViewModel()
        {
        }

        public RectangleViewModel(double x, double y, double width, double height, BitmapImage BSource, double opacity, string name)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.bImage = BSource;
            this.opacity = opacity;
            this.iSource = BSource;
            this.rectName = name;
            this.draw = new DrawingGroup();
            
            // set bounds for drawing group
            GeometryDrawing lineHolder =
                new GeometryDrawing(
                    new SolidColorBrush(Color.FromArgb(0,0,0,0)),
                    new Pen(Brushes.Green, 0),
                    new LineGeometry(new Point(0,0), new Point(width, height))
                );

           this.draw.Children.Add(lineHolder);


        }

        public RectangleViewModel(RectangleViewModel old)
        {
            this.x = old.X;
            this.y = old.Y;
            this.width = old.Width;
            this.height = old.Height;
            this.iSource = old.ISource;
            this.opacity = old.Opacity;
            this.bImage = old.BImage;
            this.draw = old.draw;
            this.rectName = old.rectName;
        }

        public void imageConvolution(double[, ,] MC, double factor, double offset)
        {

            int stride = BImage.PixelWidth * 4;
            int size = BImage.PixelHeight * stride;
            byte[] pixels = new byte[size];
            byte[] pixelDepth = new byte[4];
            pixelHolder[,] array = new pixelHolder[BImage.PixelWidth, BImage.PixelHeight];
            pixelHolder[,] finishedArray = new pixelHolder[BImage.PixelWidth, BImage.PixelHeight];


            BImage.CopyPixels(pixels, stride, 0);
            for (int y = 0; y < BImage.PixelHeight; y++)
            {
                for (int x = 0; x < BImage.PixelWidth; x++)
                {
                    int index = y * stride + 4 * x;

                    pixelHolder tempPixel = new pixelHolder(pixels[index], pixels[index + 1], pixels[index + 2], pixels[index + 3]);

                    array[x, y] = tempPixel;


                }
            }
            for (int y = 1; y < BImage.PixelHeight - 1; y++)
            {
                for (int x = 1; x < BImage.PixelWidth - 1; x++)
                {

                    double[, ,] MD = new double[3, 3, 3] { 
                                                    { { array[x-1, y-1].Blue, array[x-1, y-1].Green, array[x-1,y-1].Red }, { array[x, y-1].Blue, array[x, y-1].Green, array[x,y-1].Red }, { array[x+1, y-1].Blue, array[x+1, y-1].Green, array[x+1,y-1].Red } }, 
                                                    { { array[x-1, y].Blue, array[x-1, y].Green, array[x-1,y].Red }, { array[x, y].Blue, array[x, y].Green, array[x,y].Red }, { array[x+1, y].Blue, array[x, y].Green, array[x+1,y].Red } },
                                                    { { array[x-1, y+1].Blue, array[x-1, y+1].Green, array[x-1,y+1].Red }, { array[x, y+1].Blue, array[x, y+1].Green, array[x,y+1].Red }, { array[x+1, y+1].Blue, array[x+1, y+1].Green, array[x+1,y+1].Red } }
                    };


                    pixelHolder tempNewPixel = new pixelHolder();
                    // need to be set each new matrix

                    double tempBlue = ((((MD[0, 0, 0] * MC[0, 0, 0]) + (MD[0, 1, 0] * MC[0, 1, 0]) + (MD[0, 2, 0] * MC[0, 2, 0]) +
                                               (MD[1, 0, 0] * MC[1, 0, 0]) + (MD[1, 1, 0] * MC[1, 1, 0]) + (MD[1, 2, 0] * MC[1, 2, 0]) +
                                               (MD[2, 0, 0] * MC[2, 0, 0]) + (MD[2, 1, 0] * MC[2, 1, 0]) + (MD[2, 2, 0] * MC[2, 2, 0])) / factor) + offset);
                    double tempRed = ((((MD[0, 0, 1] * MC[0, 0, 0]) + (MD[0, 1, 1] * MC[0, 1, 0]) + (MD[0, 2, 1] * MC[0, 2, 0]) +
                                               (MD[1, 0, 1] * MC[1, 0, 0]) + (MD[1, 1, 1] * MC[1, 1, 0]) + (MD[1, 2, 1] * MC[1, 2, 0]) +
                                               (MD[2, 0, 1] * MC[2, 0, 0]) + (MD[2, 1, 1] * MC[2, 1, 0]) + (MD[2, 2, 1] * MC[2, 2, 0])) / factor) + offset);
                    double tempGreen = ((((MD[0, 0, 2] * MC[0, 0, 0]) + (MD[0, 1, 2] * MC[0, 1, 0]) + (MD[0, 2, 2] * MC[0, 2, 0]) +
                                               (MD[1, 0, 2] * MC[1, 0, 0]) + (MD[1, 1, 2] * MC[1, 1, 0]) + (MD[1, 2, 2] * MC[1, 2, 0]) +
                                               (MD[2, 0, 2] * MC[2, 0, 0]) + (MD[2, 1, 2] * MC[2, 1, 0]) + (MD[2, 2, 2] * MC[2, 2, 0])) / factor) + offset);
                    tempNewPixel.Blue = (byte)((((MD[0, 0, 0] * MC[0, 0, 0]) + (MD[0, 1, 0] * MC[0, 1, 0]) + (MD[0, 2, 0] * MC[0, 2, 0]) +
                                               (MD[1, 0, 0] * MC[1, 0, 0]) + (MD[1, 1, 0] * MC[1, 1, 0]) + (MD[1, 2, 0] * MC[1, 2, 0]) +
                                               (MD[2, 0, 0] * MC[2, 0, 0]) + (MD[2, 1, 0] * MC[2, 1, 0]) + (MD[2, 2, 0] * MC[2, 2, 0])) / factor) + offset);


                    tempNewPixel.Green = (byte)((((MD[0, 0, 1] * MC[0, 0, 0]) + (MD[0, 1, 1] * MC[0, 1, 0]) + (MD[0, 2, 1] * MC[0, 2, 0]) +
                                               (MD[1, 0, 1] * MC[1, 0, 0]) + (MD[1, 1, 1] * MC[1, 1, 0]) + (MD[1, 2, 1] * MC[1, 2, 0]) +
                                               (MD[2, 0, 1] * MC[2, 0, 0]) + (MD[2, 1, 1] * MC[2, 1, 0]) + (MD[2, 2, 1] * MC[2, 2, 0])) / factor) + offset);

                    tempNewPixel.Red = (byte)((((MD[0, 0, 2] * MC[0, 0, 0]) + (MD[0, 1, 2] * MC[0, 1, 0]) + (MD[0, 2, 2] * MC[0, 2, 0]) +
                                               (MD[1, 0, 2] * MC[1, 0, 0]) + (MD[1, 1, 2] * MC[1, 1, 0]) + (MD[1, 2, 2] * MC[1, 2, 0]) +
                                               (MD[2, 0, 2] * MC[2, 0, 0]) + (MD[2, 1, 2] * MC[2, 1, 0]) + (MD[2, 2, 2] * MC[2, 2, 0])) / factor) + offset);
                    if (tempBlue < (byte)0) tempNewPixel.Blue = (byte)0;
                    if (tempBlue > (byte)255) tempNewPixel.Blue = (byte)255;

                    if (tempRed < (byte)0) tempNewPixel.Red = (byte)0;
                    if (tempRed > (byte)255) tempNewPixel.Red = (byte)255;

                    if (tempGreen < (byte)0) tempNewPixel.Green = (byte)0;
                    if (tempGreen > (byte)255) tempNewPixel.Green = (byte)255;
                    finishedArray[x, y] = tempNewPixel;

                }
            }
            for (int y = 1; y < BImage.PixelHeight - 1; y++)
            {
                for (int x = 1; x < BImage.PixelWidth - 1; x++)
                {
                    int index = y * stride + 4 * x;

                    pixels[index] = finishedArray[x, y].Blue;
                    pixels[index + 1] = finishedArray[x, y].Green;
                    pixels[index + 2] = finishedArray[x, y].Red;
                    pixels[index + 3] = 255;
                }
            }

            ////////////helo
            BitmapPalette myPalette3 = new BitmapPalette(BImage, 128);
            BitmapSource image = BitmapSource.Create(
                BImage.PixelWidth,
                BImage.PixelHeight,
                96,
                96,
                PixelFormats.Pbgra32,
                 myPalette3,
                pixels,
                stride);

            //////
            string tempSavePath = Path.GetTempPath();
            string tempSavename = Path.GetRandomFileName();
            // char[] charsToTrim = {''};
            //tempSavename.TrimStart(charsToTrim).StartsWith(".");
            tempSavePath = tempSavePath + tempSavename;

            BitmapImage bi2 = new BitmapImage();


            using (FileStream outStream = new FileStream(tempSavePath, FileMode.Create))
            {
                // png encoder
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                // save the data to the stream
                encoder.Save(outStream);

                outStream.Close();
            }




            // http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/82a5731e-e201-4aaf-8d4b-062b138338fe
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi2.BeginInit();
            bi2.UriSource = new Uri(tempSavePath, UriKind.RelativeOrAbsolute);
            bi2.EndInit();
            BImage = bi2;

        }

        public void colorConvolution(double[, ,] MC, double _red, double _blue, double _green)
        {

            int stride = BImage.PixelWidth * 4;
            int size = BImage.PixelHeight * stride;
            byte[] pixels = new byte[size];
            byte[] pixelDepth = new byte[4];
            pixelHolder[,] array = new pixelHolder[BImage.PixelWidth, BImage.PixelHeight];
            pixelHolder[,] finishedArray = new pixelHolder[BImage.PixelWidth, BImage.PixelHeight];


            BImage.CopyPixels(pixels, stride, 0);
            for (int y = 0; y < BImage.PixelHeight; y++)
            {
                for (int x = 0; x < BImage.PixelWidth; x++)
                {
                    int index = y * stride + 4 * x;

                    pixelHolder tempPixel = new pixelHolder(pixels[index], pixels[index + 1], pixels[index + 2], pixels[index + 3]);

                    array[x, y] = tempPixel;


                }
            }

            for (int y = 1; y < BImage.PixelHeight - 1; y++)
            {
                for (int x = 1; x < BImage.PixelWidth - 1; x++)
                {
                    int index = y * stride + 4 * x;
                    double tempColor = (array[x, y].Blue * _blue) + (array[x, y].Green * _green) + (array[x, y].Red * _red);
                    pixels[index] = (byte)tempColor;
                    pixels[index + 1] = (byte)tempColor;
                    pixels[index + 2] = (byte)tempColor;
                    pixels[index + 3] = 255;
                }
            }

            ////////////helo
            BitmapPalette myPalette3 = new BitmapPalette(BImage, 128);
            BitmapSource image = BitmapSource.Create(
                BImage.PixelWidth,
                BImage.PixelHeight,
                96,
                96,
                PixelFormats.Pbgra32,
                 myPalette3,
                pixels,
                stride);

            //////
            string tempSavePath = Path.GetTempPath();
            string tempSavename = Path.GetRandomFileName();
            // char[] charsToTrim = {''};
            //tempSavename.TrimStart(charsToTrim).StartsWith(".");
            tempSavePath = tempSavePath + tempSavename;

            BitmapImage bi2 = new BitmapImage();


            using (FileStream outStream = new FileStream(tempSavePath, FileMode.Create))
            {
                // png encoder
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                // save the data to the stream
                encoder.Save(outStream);

                outStream.Close();
            }




            // http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/82a5731e-e201-4aaf-8d4b-062b138338fe
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi2.BeginInit();
            bi2.UriSource = new Uri(tempSavePath, UriKind.RelativeOrAbsolute);
            bi2.EndInit();
            BImage = bi2;

        }

        public void imageSelectionDeletionRotated(Point top, Point right, Point bottum, Point left)
        {
            // imageSelectionRotated(top, right, bottum, left, selected[i]);

            int stride = BImage.PixelWidth * 4;
            int size = BImage.PixelHeight * stride;
            byte[] pixels = new byte[size];
            byte[] pixelDepth = new byte[4];
            pixelHolder[,] array = new pixelHolder[BImage.PixelWidth, BImage.PixelHeight];



            // This will put the whole image in the array
            BImage.CopyPixels(pixels, stride, 0);
            for (int y = 0; y < BImage.PixelHeight; y++)
            {
                for (int x = 0; x < BImage.PixelWidth; x++)
                {
                    int index = y * stride + 4 * x;

                    pixelHolder tempPixel = new pixelHolder(pixels[index], pixels[index + 1], pixels[index + 2], pixels[index + 3]);

                    array[x, y] = tempPixel;
                }
            }

            // need lines



            for (int y = (int)top.Y; y < (int)(top.Y + bottum.Y); y++)
            {
                for (int x = (int)left.X; x < (int)(left.X + right.X); x++)
                {
                    double slopeNW = ((((left.Y - top.Y) / (left.X - top.X)) * (x - top.X)) + top.Y);
                    double slopeNE = ((((right.Y - top.Y) / (right.X - top.X)) * (x - top.X)) + top.Y);
                    double slopeSW = ((((left.Y - top.Y) / (left.X - top.X)) * (x - bottum.X)) + bottum.Y);
                    double slopeSE = ((((right.Y - top.Y) / (right.X - top.X)) * (x - bottum.X)) + bottum.Y);
                    // this sould test if it is in bounds
                    if ((y > slopeNW) && (y > slopeNE) && (y < slopeSW) && (y < slopeSE))
                    {
                        array[x, y].Blue = 0;
                        array[x, y].Green = 0;
                        array[x, y].Red = 0;
                        array[x, y].Alpha = 0;
                    }
                }
            }

            for (int y = 0; y < BImage.PixelHeight - 1; y++)
            {
                for (int x = 0; x < BImage.PixelWidth - 1; x++)
                {
                    int index = y * stride + 4 * x;

                    pixels[index] = (byte)(array[x, y].Blue);
                    pixels[index + 1] = (byte)(array[x, y].Green);
                    pixels[index + 2] = (byte)(array[x, y].Red);
                    pixels[index + 3] = (byte)(array[x, y].Alpha);
                }
            }
            // this is for the old Image
            BitmapPalette myPalette2 = new BitmapPalette(BImage, 128);
            BitmapSource imageOld = BitmapSource.Create(
                BImage.PixelWidth,
                BImage.PixelHeight,
                96,
                96,
                PixelFormats.Pbgra32,
                 myPalette2,
                pixels,
                stride);

            string tempSavePath1 = Path.GetTempPath();
            string tempSavename1 = Path.GetRandomFileName();
            // char[] charsToTrim = {''};
            //tempSavename.TrimStart(charsToTrim).StartsWith(".");
            tempSavePath1 = tempSavePath1 + tempSavename1;

            BitmapImage bi2Old = new BitmapImage();

            using (FileStream outStream = new FileStream(tempSavePath1, FileMode.Create))
            {
                // png encoder
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageOld));
                // save the data to the stream
                encoder.Save(outStream);

                outStream.Close();
            }

            bi2Old.BeginInit();
            bi2Old.UriSource = new Uri(tempSavePath1, UriKind.RelativeOrAbsolute);
            bi2Old.EndInit();
            BImage = bi2Old;

        }

        public String imageSelectionRetrivialRotated(Point top, Point right, Point bottum, Point left)
        {
            // imageSelectionRotated(top, right, bottum, left, selected[i]);

            int stride = BImage.PixelWidth * 4;
            int size = BImage.PixelHeight * stride;
            byte[] pixels = new byte[size];
            byte[] pixelDepth = new byte[4];
            pixelHolder[,] array = new pixelHolder[BImage.PixelWidth, BImage.PixelHeight];

            // This will put the whole image in the array
            BImage.CopyPixels(pixels, stride, 0);
            for (int y = 0; y < BImage.PixelHeight; y++)
            {
                for (int x = 0; x < BImage.PixelWidth; x++)
                {
                    int index = y * stride + 4 * x;

                    pixelHolder tempPixel = new pixelHolder(pixels[index], pixels[index + 1], pixels[index + 2], pixels[index + 3]);

                    array[x, y] = tempPixel;
                }
            }


            // Width
            int calcWidth = (int)(right.X - left.X);
            // Height
            int calcHeight = (int)(bottum.Y - top.Y);
            // New Array
            byte[] EndPixels = new byte[(calcWidth * 4) * calcHeight];




            for (int y = (int)top.Y; y < (int)(bottum.Y); y++)
            {
                for (int x = (int)left.X; x < (int)(right.X); x++)
                {
                    int index = (int)(y - top.Y) * (calcWidth * 4) + 4 * (int)(x - left.X);
                    double slopeNW = ((((left.Y - top.Y) / (left.X - top.X)) * (x - top.X)) + top.Y);
                    double slopeNE = ((((right.Y - top.Y) / (right.X - top.X)) * (x - top.X)) + top.Y);
                    double slopeSW = ((((left.Y - top.Y) / (left.X - top.X)) * (x - bottum.X)) + bottum.Y);
                    double slopeSE = ((((right.Y - top.Y) / (right.X - top.X)) * (x - bottum.X)) + bottum.Y);
                    // this sould test if it is in bounds
                    if ((y > slopeNW) && (y > slopeNE) && (y < slopeSW) && (y < slopeSE))
                    {
                        EndPixels[index] = (byte)(array[x, y].Blue);
                        EndPixels[index + 1] = (byte)(array[x, y].Green);
                        EndPixels[index + 2] = (byte)(array[x, y].Red);
                        EndPixels[index + 3] = 255;
                        //array[x, y].Blue = 0;
                        //array[x, y].Green = 0;
                        //array[x, y].Red = 0;
                        //array[x, y].Alpha = 0;
                    }else{
                        EndPixels[index] = 0;
                        EndPixels[index + 1] = 0;
                        EndPixels[index + 2] = 0;
                        EndPixels[index + 3] = 0;
                    }
                }
            }

            for (int y = 0; y < BImage.PixelHeight - 1; y++)
            {
                for (int x = 0; x < BImage.PixelWidth - 1; x++)
                {
                    int index = y * stride + 4 * x;

                    pixels[index] = (byte)(array[x, y].Blue);
                    pixels[index + 1] = (byte)(array[x, y].Green);
                    pixels[index + 2] = (byte)(array[x, y].Red);
                    pixels[index + 3] = (byte)(array[x, y].Alpha);
                }
            }
            // this is for the old Image
            BitmapPalette myPalette2 = new BitmapPalette(BImage, 128);
            BitmapSource imageOld = BitmapSource.Create(
                BImage.PixelWidth,
                BImage.PixelHeight,
                96,
                96,
                PixelFormats.Pbgra32,
                 myPalette2,
                pixels,
                stride);

            string tempSavePath1 = Path.GetTempPath();
            string tempSavename1 = Path.GetRandomFileName();
            // char[] charsToTrim = {''};
            //tempSavename.TrimStart(charsToTrim).StartsWith(".");
            tempSavePath1 = tempSavePath1 + tempSavename1;

            BitmapImage bi2Old = new BitmapImage();

            using (FileStream outStream = new FileStream(tempSavePath1, FileMode.Create))
            {
                // png encoder
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageOld));
                // save the data to the stream
                encoder.Save(outStream);

                outStream.Close();
            }

            bi2Old.BeginInit();
            bi2Old.UriSource = new Uri(tempSavePath1, UriKind.RelativeOrAbsolute);
            bi2Old.EndInit();
            BImage = bi2Old;

            // ***********************
            //this is for the new Image
            BitmapPalette myPalette3 = new BitmapPalette(BImage, 128);
            BitmapSource image = BitmapSource.Create(
                calcWidth,
                calcHeight,
                96,
                96,
                PixelFormats.Pbgra32,
                 myPalette3,
                EndPixels,
                (calcWidth * 4));

            string tempSavePath = Path.GetTempPath();
            string tempSavename = Path.GetRandomFileName();
            // char[] charsToTrim = {''};
            //tempSavename.TrimStart(charsToTrim).StartsWith(".");
            tempSavePath = tempSavePath + tempSavename;

            BitmapImage bi2 = new BitmapImage();


            using (FileStream outStream = new FileStream(tempSavePath, FileMode.Create))
            {
                // png encoder
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                // save the data to the stream
                encoder.Save(outStream);

                outStream.Close();
            }




            // http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/82a5731e-e201-4aaf-8d4b-062b138338fe
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            return tempSavePath;
}

        public String imageSelectionRetrivial(int topLeftX, int topLeftY, int width, int height)
                {


                    int stride = BImage.PixelWidth * 4;
                    int size = BImage.PixelHeight * stride;
                    byte[] pixels = new byte[size];
                    byte[] pixelDepth = new byte[4];
                    pixelHolder[,] array = new pixelHolder[BImage.PixelWidth, BImage.PixelHeight];



                    // This will put the whole image in the array
                    BImage.CopyPixels(pixels, stride, 0);
                    for (int y = 0; y < BImage.PixelHeight; y++)
                    {
                        for (int x = 0; x < BImage.PixelWidth; x++)
                        {
                            int index = y * stride + 4 * x;

                            pixelHolder tempPixel = new pixelHolder(pixels[index], pixels[index + 1], pixels[index + 2], pixels[index + 3]);

                            array[x, y] = tempPixel;
                        }
                    }



                    // Width
                    int calcWidth = width;
                    // Height
                    int calcHeight = height;
                    // New Array
                    byte[] EndPixels = new byte[(calcWidth * 4) * calcHeight];



                    for (int y = topLeftY; y < (topLeftY + height); y++)
                    {
                        for (int x = topLeftX; x < (topLeftX + width); x++)
                        {
                            int index = (y - topLeftY) * (calcWidth * 4) + 4 * (x - topLeftX);


                            EndPixels[index] = (byte)(array[x, y].Blue);
                            EndPixels[index + 1] = (byte)(array[x, y].Green);
                            EndPixels[index + 2] = (byte)(array[x, y].Red);
                            EndPixels[index + 3] = 255;
                            //array[x, y].Blue = 0;
                            // array[x, y].Green = 0;
                            //array[x, y].Red = 0;
                            // array[x, y].Alpha = 0;
                        }
                    }

                    for (int y = 0; y < BImage.PixelHeight - 1; y++)
                    {
                        for (int x = 0; x < BImage.PixelWidth - 1; x++)
                        {
                            int index = y * stride + 4 * x;

                            pixels[index] = (byte)(array[x, y].Blue);
                            pixels[index + 1] = (byte)(array[x, y].Green);
                            pixels[index + 2] = (byte)(array[x, y].Red);
                            pixels[index + 3] = (byte)(array[x, y].Alpha);
                        }
                    }
                    // this is for the old Image
                    BitmapPalette myPalette2 = new BitmapPalette(BImage, 128);
                    BitmapSource imageOld = BitmapSource.Create(
                        BImage.PixelWidth,
                        BImage.PixelHeight,
                        96,
                        96,
                        PixelFormats.Pbgra32,
                         myPalette2,
                        pixels,
                        stride);

                    string tempSavePath1 = Path.GetTempPath();
                    string tempSavename1 = Path.GetRandomFileName();
                    // char[] charsToTrim = {''};
                    //tempSavename.TrimStart(charsToTrim).StartsWith(".");
                    tempSavePath1 = tempSavePath1 + tempSavename1;

                    BitmapImage bi2Old = new BitmapImage();

                    using (FileStream outStream = new FileStream(tempSavePath1, FileMode.Create))
                    {
                        // png encoder
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(imageOld));
                        // save the data to the stream
                        encoder.Save(outStream);

                        outStream.Close();
                    }

                    bi2Old.BeginInit();
                    bi2Old.UriSource = new Uri(tempSavePath1, UriKind.RelativeOrAbsolute);
                    bi2Old.EndInit();
                    BImage = bi2Old;



                    // ***********************
                    //this is for the new Image
                    BitmapPalette myPalette3 = new BitmapPalette(BImage, 128);
                    BitmapSource image = BitmapSource.Create(
                        calcWidth,
                        calcHeight,
                        96,
                        96,
                        PixelFormats.Pbgra32,
                         myPalette3,
                        EndPixels,
                        (calcWidth * 4));

                    string tempSavePath = Path.GetTempPath();
                    string tempSavename = Path.GetRandomFileName();
                    // char[] charsToTrim = {''};
                    //tempSavename.TrimStart(charsToTrim).StartsWith(".");
                    tempSavePath = tempSavePath + tempSavename;

                    BitmapImage bi2 = new BitmapImage();


                    using (FileStream outStream = new FileStream(tempSavePath, FileMode.Create))
                    {
                        // png encoder
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(image));
                        // save the data to the stream
                        encoder.Save(outStream);

                        outStream.Close();
                    }




                    // http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/82a5731e-e201-4aaf-8d4b-062b138338fe
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.
                    return tempSavePath;

                }

        public void imageSelectionDeletion(int topLeftX, int topLeftY, int width, int height)
        {
            //  (_topLeftX, _topLeftY, _width, _height, selected[i]);

            int stride = BImage.PixelWidth * 4;
            int size = BImage.PixelHeight * stride;
            byte[] pixels = new byte[size];
            byte[] pixelDepth = new byte[4];
            pixelHolder[,] array = new pixelHolder[BImage.PixelWidth, BImage.PixelHeight];



            // This will put the whole image in the array
            BImage.CopyPixels(pixels, stride, 0);
            for (int y = 0; y < BImage.PixelHeight; y++)
            {
                for (int x = 0; x < BImage.PixelWidth; x++)
                {
                    int index = y * stride + 4 * x;

                    pixelHolder tempPixel = new pixelHolder(pixels[index], pixels[index + 1], pixels[index + 2], pixels[index + 3]);

                    array[x, y] = tempPixel;
                }
            }



            // Width
            int calcWidth = width;
            // Height
            int calcHeight = height;
            // New Array
            // byte[] EndPixels = new byte[(calcWidth*4 )* calcHeight];




            for (int y = topLeftY; y < (topLeftY + height); y++)
            {
                for (int x = topLeftX; x < (topLeftX + width); x++)
                {
                    int index = (y - topLeftY) * (calcWidth * 4) + 4 * (x - topLeftX);

                    // EndPixels[index] = (byte)(array[x, y].Blue);
                    //  EndPixels[index + 1] = (byte)(array[x, y].Green);
                    //  EndPixels[index + 2] = (byte)(array[x, y].Red);
                    //  EndPixels[index + 3] = 255;
                    array[x, y].Blue = 0;
                    array[x, y].Green = 0;
                    array[x, y].Red = 0;
                    array[x, y].Alpha = 0;
                }
            }

            for (int y = 0; y < BImage.PixelHeight - 1; y++)
            {
                for (int x = 0; x < BImage.PixelWidth - 1; x++)
                {
                    int index = y * stride + 4 * x;

                    pixels[index] = (byte)(array[x, y].Blue);
                    pixels[index + 1] = (byte)(array[x, y].Green);
                    pixels[index + 2] = (byte)(array[x, y].Red);
                    pixels[index + 3] = (byte)(array[x, y].Alpha);
                }
            }
            // this is for the old Image
            BitmapPalette myPalette2 = new BitmapPalette(BImage, 128);
            BitmapSource imageOld = BitmapSource.Create(
                BImage.PixelWidth,
                BImage.PixelHeight,
                96,
                96,
                PixelFormats.Pbgra32,
                 myPalette2,
                pixels,
                stride);

            string tempSavePath1 = Path.GetTempPath();
            string tempSavename1 = Path.GetRandomFileName();
            // char[] charsToTrim = {''};
            //tempSavename.TrimStart(charsToTrim).StartsWith(".");
            tempSavePath1 = tempSavePath1 + tempSavename1;

            BitmapImage bi2Old = new BitmapImage();

            using (FileStream outStream = new FileStream(tempSavePath1, FileMode.Create))
            {
                // png encoder
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageOld));
                // save the data to the stream
                encoder.Save(outStream);

                outStream.Close();
            }

            bi2Old.BeginInit();
            bi2Old.UriSource = new Uri(tempSavePath1, UriKind.RelativeOrAbsolute);
            bi2Old.EndInit();
            BImage = bi2Old;

        }

        /// <summary>
        /// The X coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (x == value)
                {
                    return;
                }

                x = value;

                OnPropertyChanged("X");
            }
        }

        /// <summary>
        /// The Y coordinate of the location of the rectangle (in content coordinates).
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y == value)
                {
                    return;
                }

                y = value;

                OnPropertyChanged("Y");
            }
        }

        /// <summary>
        /// The width of the rectangle (in content coordinates).
        /// </summary>
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width == value)
                {
                    return;
                }

                width = value;

                OnPropertyChanged("Width");
            }
        }

        /// <summary>
        /// The height of the rectangle (in content coordinates).
        /// </summary>
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height == value)
                {
                    return;
                }

                height = value;

                OnPropertyChanged("Height");
            }
        }

        /// <summary>
        /// The color of the item.
        /// </summary>
        public ImageSource ISource
        {
            get
            {
                return iSource;
            }
           // Don't set isource directly, use BImage

        }

        public BitmapImage BImage
        {
            get
            {
                return bImage;
            }
            set
            {
                if (bImage == value)
                {
                    return;
                }

                bImage = value;
                iSource = bImage;

                OnPropertyChanged("bImage");
                OnPropertyChanged("iSource");
            }
        }

        public double Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                if (Opacity == value)
                {
                    return;
                }

                opacity = value;

                OnPropertyChanged("Opacity");
            }
        }
        public double RAngle
        {
            get
            {
                return rAngle;
            }
            set
            {
                if (RAngle == value)
                {
                    return;
                }

                rAngle = value;

                OnPropertyChanged("RAngle");
            }
        }
        public double Scale
        {
            get
            {
                return scale;
            }
            set
            {
                if (Scale == value)
                {
                    return;
                }

                scale = value;

                OnPropertyChanged("Scale");
            }
        }

        public DrawingGroup Draw
        {
            get
            {
                return draw;
            }
            set
            {
                if (draw == value)
                {
                    return;
                }
                draw = value;

                OnPropertyChanged("Draw");
            }
        }

        /// <summary>
        /// The hotspot of the rectangle's connector.
        /// This value is pushed through from the UI because it is data-bound to 'Hotspot'
        /// in ConnectorItem.
        /// </summary>
        public Point ConnectorHotspot
        {
            get
            {
                return connectorHotspot;
            }
            set
            {
                if (connectorHotspot == value)
                {
                    return;
                }

                connectorHotspot = value;

                OnPropertyChanged("ConnectorHotspot");
            }
        }

        public string RectName
        {
            get
            {
                return rectName;
            }
            set
            {
                if (rectName == value)
                {
                    return;
                }

                rectName = value;

                OnPropertyChanged("RectName");
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the 'PropertyChanged' event when the value of a property of the view model has changed.
        /// </summary>
        protected void OnPropertyChanged(string name)
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

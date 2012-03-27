using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SampleCode
{
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
        }

        public RectangleViewModel(RectangleViewModel old)
        {
            this.x = old.X;
            this.y = old.Y;
            this.width = old.Width;
            this.height = old.Height;
            this.iSource = old.ISource;
            this.opacity = old.Opacity;
            this.rectName = old.rectName;
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

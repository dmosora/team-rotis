using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;

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
        private ImageSource iSource;

        private double opacity;

        /// <summary>
        /// The hotspot of the rectangle's connector.
        /// This value is pushed through from the UI because it is data-bound to 'Hotspot'
        /// in ConnectorItem.
        /// </summary>
        private Point connectorHotspot;

        #endregion Data Members

        public RectangleViewModel()
        {
        }

        public RectangleViewModel(double x, double y, double width, double height, ImageSource iSource, double Opacity)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.iSource = iSource;
            this.opacity = Opacity;
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
            set
            {
                if (ISource == iSource)
                {
                    return;
                }

                iSource = ISource;

                OnPropertyChanged("ImageSource");
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

                opacity = Opacity;

                OnPropertyChanged("Opacity");
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

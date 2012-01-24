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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Object;
using System.Windows.Threading.DispatcherObject;
using System.Windows.DependencyObject;
using System.Windows.Freezable;
using System.Windows.Media.Animation.Animatable;
using System.Windows.Media.Drawing;

using System.Windows.Media.DrawingGroup;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //
            // Create three drawings.
            //
            GeometryDrawing ellipseDrawing =
                new GeometryDrawing(
                    new SolidColorBrush(Color.FromArgb(102, 181, 243, 20)),
                    new Pen(Brushes.Black, 4),
                    new EllipseGeometry(new Point(50, 50), 50, 50)
                );

            ImageDrawing kiwiPictureDrawing =
                new ImageDrawing(
                    new BitmapImage(new Uri(@"sampleImages\kiwi.png", UriKind.Relative)),
                    new Rect(50, 50, 100, 100));

            GeometryDrawing ellipseDrawing2 =
                new GeometryDrawing(
                    new SolidColorBrush(Color.FromArgb(102, 181, 243, 20)),
                    new Pen(Brushes.Black, 4),
                    new EllipseGeometry(new Point(150, 150), 50, 50)
                );

            // Create a DrawingGroup to contain the drawings.
            DrawingGroup aDrawingGroup = new DrawingGroup();
            //aDrawingGroup.Children.Add(ellipseDrawing);
            //aDrawingGroup.Children.Add(kiwiPictureDrawing);
            //aDrawingGroup.Children.Add(ellipseDrawing2);
        }
    }

    //[ContentPropertyAttribute("Children")]
    public sealed class DrawingGroup : Drawing
    { }
}

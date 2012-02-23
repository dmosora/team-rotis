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
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Collections;
using Microsoft.Win32;
using System.IO;

namespace SampleCode
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        // These need to be refactored to work with the new MDI
        // interface model.
        private void topNew_Click(object sender, RoutedEventArgs e)
        {
            //Open new window with specified dimensions
            MainWindow mw = new MainWindow();
            sizeSetWindow ss = new sizeSetWindow(ref result);
            ss.ShowDialog();
            result = ss.result;
            if (result == "Cancel")
            {
                mw.Close();
            }
            else
            {
                String width = result.Substring(0, result.IndexOf("x"));
                String height = result.Substring(result.IndexOf("x") + 1);
                
                mw.listBox.Height = Convert.ToInt32(height);
                mw.listBox.Width = Convert.ToInt32(width);
                mw.Height = Convert.ToInt32(height);
                mw.Width = Convert.ToInt32(width);
                mw.Show();
            }
        }

        private void topOpen_Click(object sender, RoutedEventArgs e)
        {
            //Open a new window with the selected image
            MainWindow newWin = new MainWindow();
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                newWin.ViewModel.addRectangle(dlg.FileName);
                newWin.listBox.Height = newWin.ViewModel.Rectangles[0].Height;
                newWin.listBox.Width = newWin.ViewModel.Rectangles[0].Width;
                newWin.Height = newWin.ViewModel.Rectangles[0].Height;
                newWin.Width = newWin.ViewModel.Rectangles[0].Width;
                newWin.ViewModel.Rectangles[0].X = 0;
                newWin.ViewModel.Rectangles[0].Y = 0;
                newWin.Show();
            }
            newWin.ViewModel.saveState();
        }

        private void topSave_Click(object sender, RoutedEventArgs e)
        {
            listBox.SelectedItems.Clear();
            
            SaveFileDialog sfd = new SaveFileDialog();
            string path=null;
            if (sfd.ShowDialog() == true)
            {
                path = sfd.FileName;
            }
            Canvas surface = FindCanvas(listBox);

            if (path == null) return;

            // Stuff to save transform, not sure if we need them, but left the code here for future reference
            // Save current canvas transform
            //Transform transform = surface.LayoutTransform;
            // reset current transform (in case it is scaled or rotated)
            //surface.LayoutTransform = null;

            // Get the size of canvas
            Size size = new Size( this.ViewModel.getWidth(100000),  this.ViewModel.getHeight(100000));
            // Measure and arrange the surface
            surface.Measure(size);
            Point move = new Point(-this.ViewModel.getLeft(100000), -this.ViewModel.getTop(100000));
            surface.Arrange(new Rect(move,size));

            // Create a render bitmap
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(surface);

            // Create a file stream for saving image
            using (FileStream outStream = new FileStream(path, FileMode.Create))
            {
                // png encoder
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
                outStream.Close();
            }
           

            // Restore previously saved layout
            //surface.LayoutTransform = transform;
            surface.Arrange(new Rect(size));
            
            ViewModel.saveState();
        }

        public Canvas FindCanvas(DependencyObject obj)
        {
            //Iterate through visual tree helper to find the dislay canvas
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is Canvas)
                {
                    Canvas found = (Canvas)child;
                    if (found.Name == "DisplayCanvas")
                        return (Canvas)child;
                }

                var childOfChild = FindCanvas(child);

                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }

        private void topClose_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void topExit_Click(object sender, RoutedEventArgs e)
        {
        }

        // These need to work on the current MdiChildForm
        private void topUndo_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.undo();
        }

        private void topRedo_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.redo();
        }

        private void topNewLayer_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make layer toolbox, implement selection of layers based on that toolbox, create new blank layer in this function
        }

        // These two need changed to use the new MDI form.
        private void topLayerFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                this.ViewModel.addRectangle(dlg.FileName);
                this.ViewModel.Rectangles[ViewModel.Rectangles.Count - 1].X = 0;
                this.ViewModel.Rectangles[ViewModel.Rectangles.Count - 1].Y = 0;
            }
            this.ViewModel.saveState();
        }

        private void topResizeClick(object sender, RoutedEventArgs e)
        {
            sizeSetWindow ss = new sizeSetWindow(ref result);
            ss.ShowDialog();
            result = ss.result;
            String width = result.Substring(0, result.IndexOf("x"));
            String height = result.Substring(result.IndexOf("x") + 1);
            this.listBox.Height = Convert.ToInt32(height);
            this.listBox.Width = Convert.ToInt32(width);
        }

    }
}

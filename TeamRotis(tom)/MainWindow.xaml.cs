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
        #region Data Members

        /// <summary>
        /// Set to 'true' when the left mouse-button is down, for mode ==0 && mode ==1
        /// </summary>
        private bool isLeftMouseButtonDownOnWindow = false;
        private bool isLeftMouseDownOnRectangle = false;
        private bool isLeftMouseAndControlDownOnRectangle = false;

        /// <summary>
        /// Dragging of the selection rectangle only starts when the left mouse-button is held down and the mouse-cursor
        /// is moved more than a threshold distance.
        /// Records the location of the mouse (relative to the window) when the left-mouse button has pressed down.
        /// The threshold distance the mouse-cursor must move before drag-selection begins. for mode == 2
        /// </summary>
        private bool isDraggingSelectionRect = false;
        private Point origMouseDownPoint;
        private static readonly double DragThreshold = 5;

        /// <summary>
        /// The current operation mode, 0:slection, 1:hand/drag, 2:select/replace, 3:draw pen, 4:cirlce draw, 5:rectangle draw, 6:add text
        /// </summary>
        private int mode = 0;

        /// <summary>
        /// Saved last point when drawing pen (mode == 3)
        /// </summary>
        private Point LastPainted = new Point(-1, -1);

        /// <summary>
        /// restult string for new window
        /// </summary>
        public String result = null;

        // Set to 'true' when dragging a rectangle. replace with "mode == 2"
        private bool isDraggingRectangle = false;

        /// <summary>
        /// Layer Pallette window
        /// </summary>
        private layerPallette lp;

        #endregion Data Members

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Convenient accessor for the view model.
        /// </summary>
        private ViewModel ViewModel
        {
            get
            {
                return (ViewModel)this.DataContext;
            }
        }

        /// <summary>
        /// Event raised when the Window has loaded.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //
            // Display help text for the sample app.
            //
            /*HelpTextWindow helpTextWindow = new HelpTextWindow();
            helpTextWindow.Left = this.Left + this.Width + 5;
            helpTextWindow.Top = this.Top;
            helpTextWindow.Owner = this;
            helpTextWindow.Show();*/

            if (double.IsNaN(this.listBox.Height))
            {
                result = "Cancel";
                while (result == "Cancel")
                {
                    sizeSetWindow ss = new sizeSetWindow(ref result);
                    ss.ShowDialog();
                    result = ss.result;
                }
                String width = result.Substring(0, result.IndexOf("x"));
                String height = result.Substring(result.IndexOf("x") + 1);
                this.listBox.Height = Convert.ToInt32(height);
                this.listBox.Width = Convert.ToInt32(width);
            }
            lp = new layerPallette(ViewModel.Rectangles);
            lp.Left = System.Windows.SystemParameters.WorkArea.Right - lp.Width; // sets pallette to right edge of the monitor
            lp.Top = System.Windows.SystemParameters.WorkArea.Bottom - lp.Height; // sets the pallette to be aligned on the bottom of the monitor
            lp.Owner = this; // Set this to the window the pallette should be tied to
            lp.Show();

            listBox.Focus();
        }

        /// <summary>
        /// Event raised when the mouse is pressed down on a rectangle.
        /// </summary>
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            var rectangle = (FrameworkElement)sender;
            var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;

            isLeftMouseDownOnRectangle = true;

            switch (mode)
            {
                // slection mode
                case 0:
                // hand/drag
                case 1: //for mousedown on rectangle perform selection logic              
                    if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
                    {
                        //
                        // Control key was held down.
                        // This means that the rectangle is being added to or removed from the existing selection.
                        // Don't do anything yet, we will act on this later in the MouseUp event handler.
                        //
                        isLeftMouseAndControlDownOnRectangle = true;
                    }
                    else
                    {
                        //
                        // Control key is not held down.
                        //
                        isLeftMouseAndControlDownOnRectangle = false;

                        if (this.listBox.SelectedItems.Count == 0)
                        {
                            //
                            // Nothing already selected, select the item.
                            //
                            this.listBox.SelectedItems.Add(rectangleViewModel);
                        }
                        else if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                        {
                            // 
                            // Item is already selected, do nothing.
                            // We will act on this in the MouseUp if there was no drag operation.
                            //
                        }
                        else
                        {
                            //
                            // Item is not selected.
                            // Deselect all, and select the item.
                            //
                            this.listBox.SelectedItems.Clear();
                            this.listBox.SelectedItems.Add(rectangleViewModel);
                        }
                    }

                    rectangle.CaptureMouse();
                    origMouseDownPoint = e.GetPosition(this);
                    break;
                // draw pen
                case 3:
                    DrawCircle(sender, e);
                        rectangleViewModel.X = rectangleViewModel.X + 1;
                        rectangleViewModel.X = rectangleViewModel.X - 1;
                    break;
                // select/replace
                case 2:
                // circle
                case 4:
                // rectangle
                case 5:
                // text
                case 6:
                        isLeftMouseDownOnRectangle = true;
                    break;
                default:
                    break;
            }
            //listBox.Focus();

            rectangle.CaptureMouse();
            origMouseDownPoint = e.GetPosition(this);

            e.Handled = true;
        }

        /// <summary>
        /// Event raised when the mouse is released on a rectangle.
        /// </summary>
        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var rectangle = (FrameworkElement)sender;
            var rectangleViewModel = (RectangleViewModel)rectangle.DataContext;

            if (isLeftMouseDownOnRectangle)
            {

                switch (mode)
                {
                    // slection mode
                    case 0:
                        if (isLeftMouseAndControlDownOnRectangle)
                        {
                            //
                            // Control key was held down.
                            // Toggle the selection.
                            //
                            if (this.listBox.SelectedItems.Contains(rectangleViewModel))
                            {
                                //
                                // Item was already selected, control-click removes it from the selection.
                                //
                                this.listBox.SelectedItems.Remove(rectangleViewModel);
                            }
                            else
                            {
                                // 
                                // Item was not already selected, control-click adds it to the selection.
                                //
                                this.listBox.SelectedItems.Add(rectangleViewModel);
                            }
                        }
                        else
                        {
                            //
                            // Control key was not held down.
                            //
                            if (this.listBox.SelectedItems.Count == 1 &&
                                this.listBox.SelectedItem == rectangleViewModel)
                            {
                                //
                                // The item that was clicked is already the only selected item.
                                // Don't need to do anything.
                                //
                            }
                            else
                            {
                                //
                                // Clear the selection and select the clicked item as the only selected item.
                                //
                                this.listBox.SelectedItems.Clear();
                                this.listBox.SelectedItems.Add(rectangleViewModel);
                            }
                        }
                        rectangle.ReleaseMouseCapture();
                        isLeftMouseDownOnRectangle = false;
                        isLeftMouseAndControlDownOnRectangle = false;

                        e.Handled = true;
                        isDraggingRectangle = false;
                        break;

                    // hand/drag
                    case 1:
                        isDraggingRectangle = false;
                        isLeftMouseDownOnRectangle = false;
                        isLeftMouseAndControlDownOnRectangle = false;

                        e.Handled = true;
                        break;

                    // select/replace
                    case 2:
                    // circle
                    case 4:
                    // rectangle
                    case 5:
                    // text
                    case 6:
                        if (isDraggingSelectionRect)
                        {
                            //
                            // Drag selection has ended, apply the 'selection rectangle'.
                            //

                            isDraggingSelectionRect = false;
                            ApplyDragSelectionRect();

                            e.Handled = true;
                        }
                        break;
                    default:
                        break;
                }
            }

            rectangle.ReleaseMouseCapture();
            isLeftMouseDownOnRectangle = false;
            isLeftMouseAndControlDownOnRectangle = false;

            e.Handled = true;
            isDraggingRectangle = false;
            listBox.Focus();
        }

        /// <summary>
        /// Event raised when the mouse is moved over a rectangle.
        /// </summary>
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var rectangle1 = (FrameworkElement)sender;
            var rectangleViewModel = (RectangleViewModel)rectangle1.DataContext;

            switch (mode)
            {
                // slection mode
                case 0:
                    // don't do anything for selection
                    break;

                // hand/drag
                case 1:
                    if (isDraggingRectangle)
                    {
                        //
                        // Drag-move selected rectangles.
                        //
                        Point curMouseDownPoint = e.GetPosition(this);
                        var dragDelta = curMouseDownPoint - origMouseDownPoint;

                        origMouseDownPoint = curMouseDownPoint;

                        foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
                        {
                            rectangle.X += dragDelta.X;
                            rectangle.Y += dragDelta.Y;
                        }
                    }
                    else if (isLeftMouseDownOnRectangle)
                    {
                        {
                            //
                            // The user is left-dragging the rectangle,
                            // but don't initiate the drag operation until
                            // the mouse cursor has moved more than the threshold value.
                            //
                            Point curMouseDownPoint = e.GetPosition(this);
                            var dragDelta = curMouseDownPoint - origMouseDownPoint;
                            double dragDistance = Math.Abs(dragDelta.Length);
                            if (dragDistance > DragThreshold)
                            {
                                //
                                // When the mouse has been dragged more than the threshold value commence dragging the rectangle.
                                //
                                isDraggingRectangle = true;
                            }
                        }
                    }
                    break;

                // draw pen
                case 3:
                    if (isLeftMouseDownOnRectangle)
                    {
                        DrawLine(sender, e);
                        rectangleViewModel.X = rectangleViewModel.X + 1;
                        rectangleViewModel.X = rectangleViewModel.X - 1;
                    }
                    break;


                // select/replace
                case 2:
                // circle
                case 4:
                // rectangle
                case 5:
                    // for each manage dragging rectangle



                    if (isDraggingSelectionRect)
                    {
                        //
                        // Drag selection is in progress.
                        //
                        Point curMouseDownPoint = e.GetPosition(dragSelectionCanvas);
                        UpdateDragSelectionRect(origMouseDownPoint, curMouseDownPoint);

                        e.Handled = true;
                    }
                    else if (isLeftMouseDownOnRectangle)
                    {
                        //
                        // The user is left-dragging the mouse,
                        // but don't initiate drag selection until
                        // they have dragged past the threshold value.
                        //
                        Point curMouseDownPoint = e.GetPosition(dragSelectionCanvas);
                        var dragDelta = curMouseDownPoint - origMouseDownPoint;
                        double dragDistance = Math.Abs(dragDelta.Length);
                        if (dragDistance > DragThreshold)
                        {
                            //
                            // When the mouse has been dragged more than the threshold value commence drag selection.
                            //
                            isDraggingSelectionRect = true;
                            InitDragSelectionRect(origMouseDownPoint, curMouseDownPoint);
                        }

                        e.Handled = true;


                    }
                    break;

                // text
                case 6:
                    //?
                    break;
                default:
                    break;
            }
            e.Handled = true;

        }

        /// <summary>
        /// Event raised when the user presses down the left mouse-button.
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                switch (mode)
                {
                    // slection mode
                    case 0:
                        // only selection cares about mouse down not on canvas
                        listBox.SelectedItems.Clear();
                        break;
                    default:
                        break;
                }

                isLeftMouseButtonDownOnWindow = true;
                origMouseDownPoint = e.GetPosition(dragSelectionCanvas);

                this.CaptureMouse();

                e.Handled = true;
            }

            listBox.Focus();
        }

        /// <summary>
        /// Event raised when the user releases the left mouse-button.
        /// </summary>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                switch (mode)
                {
                    // slection mode
                    case 0:
                        if (isLeftMouseButtonDownOnWindow)
                        {
                            isLeftMouseButtonDownOnWindow = false;
                            this.ReleaseMouseCapture();

                            e.Handled = true;
                        }
                        //
                        // A click and release in empty space clears the selection.
                        //
                        listBox.SelectedItems.Clear();
                        break;
                    // hand/drag
                    case 1:
                        break;
                    // draw pen
                    case 3:
                        break;

                    // select/replace
                    case 2:
                    // circle
                    case 4:
                    // rectangle
                    case 5:
                    // text
                    case 6:
                        if (isDraggingSelectionRect)
                        {
                            //
                            // Drag selection has ended, apply the 'selection rectangle'.
                            //

                            isDraggingSelectionRect = false;
                            ApplyDragSelectionRect();

                            e.Handled = true;
                        }
                        break;
                    default:
                        break;
                }
                isLeftMouseButtonDownOnWindow = false;
                this.ReleaseMouseCapture();

                e.Handled = true;
            }
            listBox.Focus();
        }

        /// <summary>
        /// Event raised when the user moves the mouse button.
        /// </summary>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                // select/replace
                case 2:
                // circle
                case 4:
                // rectangle
                case 5:
                    // for each manage dragging rectangle



                    if (isDraggingSelectionRect)
                    {
                        //
                        // Drag selection is in progress.
                        //
                        Point curMouseDownPoint = e.GetPosition(dragSelectionCanvas);
                        UpdateDragSelectionRect(origMouseDownPoint, curMouseDownPoint);

                        e.Handled = true;
                    }
                    else if (isLeftMouseButtonDownOnWindow)
                    {
                        //
                        // The user is left-dragging the mouse,
                        // but don't initiate drag selection until
                        // they have dragged past the threshold value.
                        //
                        Point curMouseDownPoint = e.GetPosition(dragSelectionCanvas);
                        var dragDelta = curMouseDownPoint - origMouseDownPoint;
                        double dragDistance = Math.Abs(dragDelta.Length);
                        if (dragDistance > DragThreshold)
                        {
                            //
                            // When the mouse has been dragged more than the threshold value commence drag selection.
                            //
                            isDraggingSelectionRect = true;
                            InitDragSelectionRect(origMouseDownPoint, curMouseDownPoint);
                        }

                        e.Handled = true;


                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Initialize the rectangle used for drag selection.
        /// </summary>
        private void InitDragSelectionRect(Point pt1, Point pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);

            dragSelectionCanvas.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Update the position and size of the rectangle used for drag selection.
        /// </summary>
        private void UpdateDragSelectionRect(Point pt1, Point pt2)
        {
            double x, y, width, height;

            //
            // Determine x,y,width and height of the rect inverting the points if necessary.
            // 

            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }

            //
            // Update the coordinates of the rectangle used for drag selection.
            //
            Canvas.SetLeft(dragSelectionBorder, x);
            Canvas.SetTop(dragSelectionBorder, y);
            dragSelectionBorder.Width = width;
            dragSelectionBorder.Height = height;
        }

        /// <summary>
        /// Select all nodes that are in the drag selection rectangle.
        /// </summary>
        private void ApplyDragSelectionRect()
        {
            dragSelectionCanvas.Visibility = Visibility.Collapsed;

            double x = Canvas.GetLeft(dragSelectionBorder);
            double y = Canvas.GetTop(dragSelectionBorder);
            double width = dragSelectionBorder.Width;
            double height = dragSelectionBorder.Height;
            Rect dragRect = new Rect(x, y, width, height);

            RectangleViewModel current;

            switch (mode)
            {
                // slection mode
                case 0:
                    //
                    // Inflate the drag selection-rectangle by 1/10 of its size to 
                    // make sure the intended item is selected.
                    //
                    dragRect.Inflate(width / 10, height / 10);

                    //
                    // Clear the current selection.
                    //
                    listBox.SelectedItems.Clear();

                    //
                    // Find and select all the list box items.
                    //
                    foreach (RectangleViewModel rectangleViewModel in this.ViewModel.Rectangles)
                    {
                        Rect itemRect = new Rect(rectangleViewModel.X, rectangleViewModel.Y, rectangleViewModel.Width, rectangleViewModel.Height);
                        if (dragRect.Contains(itemRect))
                        {
                            listBox.SelectedItems.Add(rectangleViewModel);
                        }
                    }
                    break;
                // hand/drag
                case 1:
                    MessageBox.Show("Hand Drag tool shouldn't call this method.", "Logic error", MessageBoxButton.OK);
                    break;
                // select/replace
                case 2:
                    RectangleViewModel[] selected = new RectangleViewModel[1];
                    int j = 0;
                    int toGrayscale = -1;
                    foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
                    {
                        int angle = ((int)Math.Ceiling(rectangle.RAngle))%360;
                        if (angle == 0)
                        {
                            // call cut function with x,y,height,width

                                selected[j] = rectangle;
                                j++;
                                toGrayscale++;


                                x = Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (x - (rectangle.X) - 50) / (rectangle.Scale) - Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (y - (rectangle.Y) - 25) / (rectangle.Scale);
                                y = Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (x - (rectangle.X) - 50) / (rectangle.Scale) + Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (y - (rectangle.Y) - 25) / (rectangle.Scale);
                            for (int i = toGrayscale; i > -1; --i)
                            {
                                int _TopLeft = (int)x;
                                int _TopRight = (int)(x + width);
                                int _BottomLeft = (int)y;
                                int _BottomRight = 0;

                                //  Top Left  = x
                                //  Top Right = x + width
                                //  Bottomm Left = y
                                //  bottom right = 0

                                // Somehow get my 4 points
                                this.ViewModel.imageSelection(_TopLeft, _TopRight, _BottomLeft, _BottomRight, selected[i]);


                                // this.ViewModel.addRectangle(rectangleToCreate);
                                //double TopLeft, double TopRight, double BottomLeft, double BottomRight

                            }
                            
                        }
                        else
                        {
                            //setting points for selection call
                            // ony if tilted

                            Point top = new Point(0, 0); Point right = new Point(0, 0); Point left = new Point(0, 0); Point bottum = new Point(0, 0);
                            if (angle > 0 && angle <= 90)
                            {
                                top = new Point(x + width, y);
                                right = new Point(x + width, y + height);
                                bottum = new Point(x, y + height);
                                left = new Point(x, y);
                            }
                            if (angle > 90 && angle <= 180)
                            {
                                top = new Point(x + width, y + height);
                                right = new Point(x, y + height);
                                bottum = new Point(x, y);
                                left = new Point(x + width, y);
                            }
                            if (angle > 180 && angle <= 270)
                            {
                                top = new Point(x, y + height);
                                right = new Point(x, y);
                                bottum = new Point(x+width, y);
                                left = new Point(x+width, y+height);
                            }
                            if (angle > 270)
                            {
                                top = new Point(x, y);
                                right = new Point(x + width, y);
                                bottum = new Point(x+width, y + height);
                                left = new Point(x, y+height);
                            }
                            top.X = Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (top.X - (rectangle.X) - 50) / (rectangle.Scale) - Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (top.Y - (rectangle.Y) - 25) / (rectangle.Scale);
                            top.Y = Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (top.X - (rectangle.X) - 50) / (rectangle.Scale) + Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (top.Y - (rectangle.Y) - 25) / (rectangle.Scale);

                            right.X = Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (right.X - (rectangle.X) - 50) / (rectangle.Scale) - Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (right.Y - (rectangle.Y) - 25) / (rectangle.Scale);
                            right.Y = Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (right.X - (rectangle.X) - 50) / (rectangle.Scale) + Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (right.Y - (rectangle.Y) - 25) / (rectangle.Scale);

                            bottum.X = Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (bottum.X - (rectangle.X) - 50) / (rectangle.Scale) - Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (bottum.Y - (rectangle.Y) - 25) / (rectangle.Scale);
                            bottum.Y = Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (bottum.X - (rectangle.X) - 50) / (rectangle.Scale) + Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (bottum.Y - (rectangle.Y) - 25) / (rectangle.Scale);

                            left.X = Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (left.X - (rectangle.X) - 50) / (rectangle.Scale) - Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (left.Y - (rectangle.Y) - 25) / (rectangle.Scale);
                            left.Y = Math.Sin(-(rectangle.RAngle * Math.PI / 180)) * (left.X - (rectangle.X) - 50) / (rectangle.Scale) + Math.Cos(-(rectangle.RAngle * Math.PI / 180)) * (left.Y - (rectangle.Y) - 25) / (rectangle.Scale);

                            // call select function
                        }

                         
                    }
                    break;
                // draw pen
                case 3:
                    MessageBox.Show("Hand Drag tool shouldn't call this method.", "Logic error", MessageBoxButton.OK);
                    break;
                // circle
                case 4:
                    current = ViewModel.addRectangle(x-50, y+20, width, height);
                    ViewModel.addOval(current, new Point((width/2), (height/2)), width, height);
                    break;
                // rectangle
                case 5:
                    current = ViewModel.addRectangle(x-50, y+20, width, height);
                    ViewModel.addRect(current, new Point(0, 0), width, height);
                    break;
                // text
                case 6:
                    current = ViewModel.addRectangle(x-50, y+20, width, height);
                    string entry="test"; // = textentry
                    ViewModel.addString(current, new Point(x + (width / 2) - 50, y + (height / 2) - 50), entry);
                    break;
            }
        }

        private void rectDelete_Click(object sender, RoutedEventArgs e)
        {
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int todelete = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                todelete++;
            }
            for (int i = todelete; i > -1; --i)
            {
                this.ViewModel.deleteRectangle(selected[i]);
            }

            ViewModel.saveState();
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg =
            new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                this.ViewModel.addRectangle(dlg.FileName);
                lp.UpdateLayers(ViewModel.Rectangles);
            }
        }

        private void gridNewRect_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.addRectangle(listBox.Width, listBox.Height);
            this.ViewModel.Rectangles[ViewModel.Rectangles.Count - 1].X = 0;
            this.ViewModel.Rectangles[ViewModel.Rectangles.Count - 1].Y = 0;
            lp.UpdateLayers(ViewModel.Rectangles);
            this.ViewModel.saveState();
        }

        private void topNew_Click(object sender, RoutedEventArgs e)
        {
            //Open new window
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void topOpen_Click(object sender, RoutedEventArgs e)
        {
            //Open a new window with the selected image
            MainWindow newWin = new MainWindow();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.GIF)|*.BMP;*.JPG;*.PNG;*.GIF|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
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
                lp.UpdateLayers(ViewModel.Rectangles);
            }
            else
            {
                newWin.Close();
            }
            newWin.ViewModel.saveState();
        }

        private void topSave_Click(object sender, RoutedEventArgs e)
        {
            listBox.SelectedItems.Clear();

            SaveFileDialog sfd = new SaveFileDialog();
            string path = null;
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
            Size size = new Size(surface.ActualWidth, surface.ActualHeight);

            // Measure and arrange the surface
            surface.Measure(size);
            Point move = new Point(-1, -1);
            surface.Arrange(new Rect(move, size));

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
            this.Close();
        }

        private void topExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void topUndo_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.undo();
        }

        private void topRedo_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.redo();
        }

        private void rectMove_Click(object sender, RoutedEventArgs e)
        {
            RectangleViewModel selected = new RectangleViewModel();
            int count = 0;
            // get every selected rectangle
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                count++;
                if (count > 1)
                {
                    //Message Box for too many selected
                    string messageBoxText = "Too many layers selected, only move one to front";
                    string caption = "Move Error";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                    break;
                }
                selected = rectangle;
            }
            // clear selections and make top layer
            this.listBox.SelectedItems.Clear();
            this.ViewModel.makeTopLayer(selected);
            lp.UpdateLayers(ViewModel.Rectangles);
            listBox.Focus();

            ViewModel.saveState();
        }

        private void topNewLayer_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.addRectangle(listBox.Width, listBox.Height);
            this.ViewModel.Rectangles[ViewModel.Rectangles.Count - 1].X = 0;
            this.ViewModel.Rectangles[ViewModel.Rectangles.Count - 1].Y = 0;
            lp.UpdateLayers(ViewModel.Rectangles);
            this.ViewModel.saveState();
        }

        private void topLayerFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                this.ViewModel.addRectangle(dlg.FileName);
                this.ViewModel.Rectangles[ViewModel.Rectangles.Count - 1].X = 0;
                this.ViewModel.Rectangles[ViewModel.Rectangles.Count - 1].Y = 0;
                lp.UpdateLayers(ViewModel.Rectangles);
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

        private void topLayerShowPalette_Click(object sender, RoutedEventArgs e)
        {
            lp.Visibility = System.Windows.Visibility.Visible;
        }

        private void rectName_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            RectangleViewModel rectTemp = new RectangleViewModel();
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                count++;
                if (count > 1)
                {
                    //Message Box for too many selected
                    string messageBoxText = "Too many layers selected, you can only rename on layer";
                    string caption = "Rename Error";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                    break;
                }
                rectTemp = (RectangleViewModel)this.listBox.SelectedItem;
            }
            string newName = "";

            nameChanger nameChangeWin = new nameChanger(ref newName);
            nameChangeWin.ShowDialog();
            newName = nameChangeWin.result;
            if (newName != "")
            {
                ViewModel.Rectangles[ViewModel.Rectangles.IndexOf(rectTemp)].RectName = newName;
                lp.UpdateLayers(ViewModel.Rectangles);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                if (e.Key == Key.N)
                {
                    topNewLayer_Click(sender, e);
                    e.Handled = true;
                }
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (e.Key == Key.N)
                {
                    topNew_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.Key == Key.O)
                {
                    topOpen_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.Key == Key.W)
                {
                    topClose_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.Key == Key.S)
                {
                    topSave_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.Key == Key.Z)
                {
                    topUndo_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.Key == Key.Y)
                {
                    topRedo_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.Key == Key.C)
                {
                    rectCopy_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.Key == Key.V)
                {
                    rectPaste_Click(sender, e);
                    e.Handled = true;
                }
                else if (e.Key == Key.X)
                {
                    rectCut_Click(sender, e);
                    e.Handled = true;
                }
            }
        }

        private void topEmboss_Click(object sender, RoutedEventArgs e)
        {

            //Were going to make grayscale
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int toGrayscale = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toGrayscale++;
            }
            //this just tosses each to the viewModel
            for (int i = toGrayscale; i > -1; --i)
            {

                this.ViewModel.effectEmboss(selected[i]);


            }
            //http://msdn.microsoft.com/en-us/library/ms750596.aspx
        }

        private void topBlur_Click(object sender, RoutedEventArgs e)
        {

            //Were going to make grayscale
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int toGrayscale = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toGrayscale++;
            }
            //this just tosses each to the viewModel
            for (int i = toGrayscale; i > -1; --i)
            {

                this.ViewModel.effectGaussianBlur(selected[i]);


            }
        }

        private void topSmooth_Click(object sender, RoutedEventArgs e)
        {

            //Were going to make grayscale
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int toGrayscale = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toGrayscale++;
            }
            //this just tosses each to the viewModel
            for (int i = toGrayscale; i > -1; --i)
            {

                this.ViewModel.effectSmoothing(selected[i]);


            }
            //http://msdn.microsoft.com/en-us/library/ms750596.aspx
        }

        private void topEdge_Click(object sender, RoutedEventArgs e)
        {

            //Were going to make grayscale
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int toGrayscale = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toGrayscale++;
            }
            //this just tosses each to the viewModel
            for (int i = toGrayscale; i > -1; --i)
            {

                this.ViewModel.effectEdgeDetection(selected[i]);


            }
        }

        private void topGrey_Click(object sender, RoutedEventArgs e)
        {

            //Were going to make grayscale
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int toGrayscale = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toGrayscale++;
            }
            //this just tosses each to the viewModel
            for (int i = toGrayscale; i > -1; --i)
            {

                this.ViewModel.effectGrayscale(selected[i]);


            }
        }

        private void rectResize_Click(object sender, RoutedEventArgs e)
        {
            if ((this.listBox.SelectedItems.Count > 1) || (this.listBox.SelectedItems.Count == 0))
            {
                MessageBox.Show("Please select only one layer to scale.", "Scale Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                resizeWindow rsw = new resizeWindow(ref result, "Scale");
                rsw.ShowDialog();
                result = rsw.result;
                if (result != "Cancel")
                {
                    this.ViewModel.Rectangles[ViewModel.Rectangles.IndexOf((RectangleViewModel)this.listBox.SelectedItem)].Scale = Convert.ToDouble(result);
                }
            }
        }
        private void rectRotate_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedItems.Count > 1)
            {
                MessageBox.Show("Please select only one layer to rotate.", "Scale Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                resizeWindow rsw = new resizeWindow(ref result, "Rotation");
                rsw.ShowDialog();
                result = rsw.result;
                if (result != "Cancel")
                {
                    this.ViewModel.Rectangles[ViewModel.Rectangles.IndexOf((RectangleViewModel)this.listBox.SelectedItem)].RAngle = Convert.ToDouble(result);
                }
            }
        }
        private void rectOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox.SelectedItems.Count > 1)
            {
                MessageBox.Show("Please select only one layer to modify.", "Scale Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                resizeWindow rsw = new resizeWindow(ref result, "Opacity");
                rsw.ShowDialog();
                result = rsw.result;
                if (result != "Cancel")
                {
                    this.ViewModel.Rectangles[ViewModel.Rectangles.IndexOf((RectangleViewModel)this.listBox.SelectedItem)].Opacity = Convert.ToDouble(result);
                }
            }
        }
        private void zoomText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (zoomText.Text.Substring(zoomText.Text.Length - 1, 1) == "%")
                {
                    zoomSlider.Value = Convert.ToDouble(zoomText.Text.Substring(0, zoomText.Text.Length - 1)) / 100;
                }
                else
                {
                    zoomSlider.Value = Convert.ToDouble(zoomText.Text) / 100;
                }
            }
        }
        private void rectCopy_Click(object sender, RoutedEventArgs e)
        {
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                if (j >= selected.Length)
                {
                    Array.Resize<RectangleViewModel>(ref selected, (selected.Length * 2));
                }
                selected[j] = rectangle;
                j++;
            }
            ViewModel.copy(selected);
        }
        private void rectPaste_Click(object sender, RoutedEventArgs e)
        {
            Point origMouseDownPoint = Mouse.GetPosition(this);
            ViewModel.paste(origMouseDownPoint);
        }
        private void rectCut_Click(object sender, RoutedEventArgs e)
        {
            rectCopy_Click(sender, e);
            rectDelete_Click(sender, e);
        }

        private void CutFromSelection_Click(object sender, RoutedEventArgs e)
        {

            // This is just to safe guard against multiple selection 
            RectangleViewModel[] selected = new RectangleViewModel[1];
            int j = 0;
            int toGrayscale = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toGrayscale++;
            }
            for (int i = toGrayscale; i > -1; --i)
            {
                int _TopLeft = 200;
                int _TopRight = 600;
                int _BottomLeft = 600;
                int BottomRight = 0;

                // Somehow get my 4 points
                this.ViewModel.imageSelection(_TopLeft, _TopRight, _BottomLeft, BottomRight, selected[i]);


                // this.ViewModel.addRectangle(rectangleToCreate);
                //double TopLeft, double TopRight, double BottomLeft, double BottomRight

            }

        }


        public void DrawCircle(object sender, RoutedEventArgs e)
        {
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int toaddto = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toaddto++;
            }

            Point clicked = new Point();
            //this just tosses each to the viewModel
            for (int i = toaddto; i > -1; --i)
            {
                clicked.X = Math.Cos(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).X - (selected[i].X) - 50) / (selected[i].Scale) - Math.Sin(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).Y - (selected[i].Y) - 25) / (selected[i].Scale);
                clicked.Y = Math.Sin(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).X - (selected[i].X) - 50) / (selected[i].Scale) + Math.Cos(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).Y - (selected[i].Y) - 25) / (selected[i].Scale);

                // if mouse is in rectangle, draw text
                if ((clicked.X > 3) && (clicked.X < selected[i].Width - 3) && (clicked.Y > 3) && (clicked.Y < selected[i].Height - 3))
                {
                    this.ViewModel.addCircle(selected[i], clicked);
                    LastPainted = Mouse.GetPosition(this);
                }
            }
        }
        public void DrawLine(object sender, RoutedEventArgs e)
        {
            if ((LastPainted.X == -1) || (LastPainted.Y == -1))
            {
                DrawCircle(sender, e);
                return;
            }
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int toaddto = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toaddto++;
            }

            Point clicked = Mouse.GetPosition(this);
            Point relativeLast = LastPainted;

            //this just tosses each to the viewModel
            for (int i = toaddto; i > -1; --i)
            {

                clicked.X = Math.Cos(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).X - (selected[i].X)-50) / (selected[i].Scale) - Math.Sin(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).Y - (selected[i].Y) - 25) / (selected[i].Scale);
                clicked.Y = Math.Sin(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).X - (selected[i].X)-50) / (selected[i].Scale) + Math.Cos(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).Y - (selected[i].Y) - 25) / (selected[i].Scale);

                relativeLast.X = Math.Cos(-(selected[i].RAngle * Math.PI / 180)) * (LastPainted.X - (selected[i].X)-50) / (selected[i].Scale) - Math.Sin(-(selected[i].RAngle * Math.PI / 180)) * ((LastPainted.Y - (selected[i].Y) - 25) / (selected[i].Scale));
                relativeLast.Y = Math.Sin(-(selected[i].RAngle * Math.PI / 180)) * (LastPainted.X - (selected[i].X)-50) / (selected[i].Scale) + Math.Cos(-(selected[i].RAngle * Math.PI / 180)) * ((LastPainted.Y - (selected[i].Y) - 25) / (selected[i].Scale));


                // if mouse is in rectangle, draw text
                if ((clicked.X > 3) && (clicked.X < selected[i].Width - 3) && (clicked.Y > 3) && (clicked.Y < selected[i].Height - 3))
                {
                    this.ViewModel.addLine(selected[i], relativeLast, clicked);
                    this.ViewModel.addCircle(selected[i], clicked);
                    LastPainted = Mouse.GetPosition(this);
                }
            }
        }

        public void DrawText_click(object sender, RoutedEventArgs e)
        {
            RectangleViewModel[] selected = new RectangleViewModel[20];
            int j = 0;
            int toaddto = -1;
            foreach (RectangleViewModel rectangle in this.listBox.SelectedItems)
            {
                selected[j] = rectangle;
                j++;
                toaddto++;
            }

            Point clicked = Mouse.GetPosition(this);

            for (int i = toaddto; i > -1; --i)
            {
                clicked.X = Math.Cos(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).X - (selected[i].X) - 50) / (selected[i].Scale) - Math.Sin(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).Y - (selected[i].Y) - 25) / (selected[i].Scale);
                clicked.Y = Math.Sin(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).X - (selected[i].X) - 50) / (selected[i].Scale) + Math.Cos(-(selected[i].RAngle * Math.PI / 180)) * (Mouse.GetPosition(this).Y - (selected[i].Y) - 25) / (selected[i].Scale);

                // if mouse is in rectangle, draw text
                if ((clicked.X > 3) && (clicked.X < selected[i].Width - 3) && (clicked.Y > 3) && (clicked.Y < selected[i].Height - 3))
                {
                    this.ViewModel.addString(selected[i], clicked, "HI!");
                }
            }
        }

        private void selectToolButton_Click(object sender, RoutedEventArgs e)
        {
            mode = 0;
            e.Handled = true;
            listBox.Focus();
        }

        private void handToolButton_Click(object sender, RoutedEventArgs e)
        {
            mode = 1;
            e.Handled = true;
            listBox.Focus();
        }

        private void pencilToolButton_Click(object sender, RoutedEventArgs e)
        {
            mode = 3;
            e.Handled = true;
            listBox.Focus();
        }

        private void rectSelectionToolButton_Click(object sender, RoutedEventArgs e)
        {
            mode = 2;
            e.Handled = true;
            listBox.Focus();
        }

        private void drawRectangleToolButton_Click(object sender, RoutedEventArgs e)
        {
            mode = 4;
            e.Handled = true;
            listBox.Focus();
        }

        private void drawOvalToolButton_Click(object sender, RoutedEventArgs e)
        {
            mode = 5;
            e.Handled = true;
            listBox.Focus();
        }

        private void textToolButton_Click(object sender, RoutedEventArgs e)
        {
            mode = 6;
            e.Handled = true;
            listBox.Focus();
        }
        
    }
}

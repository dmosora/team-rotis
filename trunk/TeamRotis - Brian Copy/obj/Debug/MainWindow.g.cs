﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "11C692C790B0BBB1E9B62C97F62D6F07"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SampleCode;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SampleCode {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 154 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button selectToolButton;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button handToolButton;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button pencilToolButton;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button rectSelectionToolButton;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button drawRectangleToolButton;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button drawOvalToolButton;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button textToolButton;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button paletteToolButton;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Slider zoomSlider;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.TextBox zoomText;
        
        #line default
        #line hidden
        
        
        #line 222 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.ListBox listBox;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem rectPaste;
        
        #line default
        #line hidden
        
        
        #line 228 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem gridNewRect;
        
        #line default
        #line hidden
        
        
        #line 268 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Canvas dragSelectionCanvas;
        
        #line default
        #line hidden
        
        
        #line 271 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Border dragSelectionBorder;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SampleCode;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\MainWindow.xaml"
            ((SampleCode.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\MainWindow.xaml"
            ((SampleCode.MainWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            
            #line 10 "..\..\MainWindow.xaml"
            ((SampleCode.MainWindow)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseUp);
            
            #line default
            #line hidden
            
            #line 11 "..\..\MainWindow.xaml"
            ((SampleCode.MainWindow)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Window_MouseMove);
            
            #line default
            #line hidden
            
            #line 12 "..\..\MainWindow.xaml"
            ((SampleCode.MainWindow)(target)).AddHandler(System.Windows.Input.Keyboard.KeyDownEvent, new System.Windows.Input.KeyEventHandler(this.Window_KeyDown));
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 107 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topNew_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 108 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topNewLayer_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 109 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topOpen_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 110 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topSave_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 111 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topClose_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 112 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topExit_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 115 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topUndo_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 116 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topRedo_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 129 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topResizeClick);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 134 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topLayerFromFile_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 135 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectDelete_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 136 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectName_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 137 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectMove_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 138 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectResize_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 139 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectRotate_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 140 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectOpacity_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 142 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topLayerShowPalette_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 145 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topEmboss_Click);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 146 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topBlur_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 147 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topSmooth_Click);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 148 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topEdge_Click);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 149 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topGrey_Click);
            
            #line default
            #line hidden
            return;
            case 30:
            this.selectToolButton = ((System.Windows.Controls.Button)(target));
            
            #line 154 "..\..\MainWindow.xaml"
            this.selectToolButton.Click += new System.Windows.RoutedEventHandler(this.selectToolButton_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            this.handToolButton = ((System.Windows.Controls.Button)(target));
            
            #line 159 "..\..\MainWindow.xaml"
            this.handToolButton.Click += new System.Windows.RoutedEventHandler(this.handToolButton_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            this.pencilToolButton = ((System.Windows.Controls.Button)(target));
            
            #line 164 "..\..\MainWindow.xaml"
            this.pencilToolButton.Click += new System.Windows.RoutedEventHandler(this.pencilToolButton_Click);
            
            #line default
            #line hidden
            return;
            case 33:
            this.rectSelectionToolButton = ((System.Windows.Controls.Button)(target));
            
            #line 169 "..\..\MainWindow.xaml"
            this.rectSelectionToolButton.Click += new System.Windows.RoutedEventHandler(this.rectSelectionToolButton_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            this.drawRectangleToolButton = ((System.Windows.Controls.Button)(target));
            
            #line 174 "..\..\MainWindow.xaml"
            this.drawRectangleToolButton.Click += new System.Windows.RoutedEventHandler(this.drawRectangleToolButton_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            this.drawOvalToolButton = ((System.Windows.Controls.Button)(target));
            
            #line 179 "..\..\MainWindow.xaml"
            this.drawOvalToolButton.Click += new System.Windows.RoutedEventHandler(this.drawOvalToolButton_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            this.textToolButton = ((System.Windows.Controls.Button)(target));
            
            #line 184 "..\..\MainWindow.xaml"
            this.textToolButton.Click += new System.Windows.RoutedEventHandler(this.textToolButton_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            this.paletteToolButton = ((System.Windows.Controls.Button)(target));
            
            #line 189 "..\..\MainWindow.xaml"
            this.paletteToolButton.Click += new System.Windows.RoutedEventHandler(this.paletteToolButton_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            this.zoomSlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 39:
            this.zoomText = ((System.Windows.Controls.TextBox)(target));
            
            #line 217 "..\..\MainWindow.xaml"
            this.zoomText.KeyUp += new System.Windows.Input.KeyEventHandler(this.zoomText_KeyUp);
            
            #line default
            #line hidden
            return;
            case 40:
            this.listBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 41:
            this.rectPaste = ((System.Windows.Controls.MenuItem)(target));
            
            #line 227 "..\..\MainWindow.xaml"
            this.rectPaste.Click += new System.Windows.RoutedEventHandler(this.rectPaste_Click);
            
            #line default
            #line hidden
            return;
            case 42:
            this.gridNewRect = ((System.Windows.Controls.MenuItem)(target));
            
            #line 228 "..\..\MainWindow.xaml"
            this.gridNewRect.Click += new System.Windows.RoutedEventHandler(this.gridNewRect_Click);
            
            #line default
            #line hidden
            return;
            case 43:
            this.dragSelectionCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 44:
            this.dragSelectionBorder = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 39 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectDelete_Click);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 40 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectCut_Click);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 41 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectCopy_Click);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 42 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectMove_Click);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 49 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            
            #line 50 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseUp);
            
            #line default
            #line hidden
            
            #line 51 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseMove);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 67 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            
            #line 68 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseUp);
            
            #line default
            #line hidden
            
            #line 69 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseMove);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}


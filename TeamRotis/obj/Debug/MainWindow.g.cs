﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DCB653BF666C5AB3C68711E75EAC5F8C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
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
        
        
        #line 124 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.ListBox listBox;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem gridCut;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.MenuItem gridNewRect;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Canvas dragSelectionCanvas;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\MainWindow.xaml"
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
            return;
            case 8:
            
            #line 84 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topNew_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 85 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topNewLayer_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 86 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topOpen_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 87 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topSave_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 92 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topUndo_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 93 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topRedo_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 106 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topResizeClick);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 111 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.topLayerFromFile_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.listBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 17:
            this.gridCut = ((System.Windows.Controls.MenuItem)(target));
            
            #line 129 "..\..\MainWindow.xaml"
            this.gridCut.Click += new System.Windows.RoutedEventHandler(this.gridCut_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.gridNewRect = ((System.Windows.Controls.MenuItem)(target));
            
            #line 130 "..\..\MainWindow.xaml"
            this.gridNewRect.Click += new System.Windows.RoutedEventHandler(this.gridNewRect_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.dragSelectionCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 20:
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
            
            #line 37 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectDelete_Click);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 38 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectCopy_Click);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 39 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectPaste_Click);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 40 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.rectMove_Click);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 41 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.editDialogBox_Click);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 48 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            
            #line 49 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseUp);
            
            #line default
            #line hidden
            
            #line 50 "..\..\MainWindow.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseMove);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

﻿#pragma checksum "..\..\sizeSetWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "50C64EF7F133668B6F254921BCE4BD46"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// sizeSetWindow
    /// </summary>
    public partial class sizeSetWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\sizeSetWindow.xaml"
        internal System.Windows.Controls.TextBox widthText;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\sizeSetWindow.xaml"
        internal System.Windows.Controls.TextBox heightText;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\sizeSetWindow.xaml"
        internal System.Windows.Controls.TextBlock widthTextLab;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\sizeSetWindow.xaml"
        internal System.Windows.Controls.TextBlock heightTextLab;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\sizeSetWindow.xaml"
        internal System.Windows.Controls.Button okayBut;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\sizeSetWindow.xaml"
        internal System.Windows.Controls.Button cancelBut;
        
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
            System.Uri resourceLocater = new System.Uri("/SampleCode;component/sizesetwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\sizeSetWindow.xaml"
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
            this.widthText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.heightText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.widthTextLab = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.heightTextLab = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.okayBut = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\sizeSetWindow.xaml"
            this.okayBut.Click += new System.Windows.RoutedEventHandler(this.okayBut_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cancelBut = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\sizeSetWindow.xaml"
            this.cancelBut.Click += new System.Windows.RoutedEventHandler(this.cancelBut_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


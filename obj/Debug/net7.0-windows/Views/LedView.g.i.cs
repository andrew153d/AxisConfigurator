﻿#pragma checksum "..\..\..\..\Views\LedView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4A09CE6DCE8CB6D1169CF9370C7432375FAA969A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AxisConfigurator;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using System.Windows.Shell;


namespace AxisConfigurator.Views {
    
    
    /// <summary>
    /// LedView
    /// </summary>
    public partial class LedView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas DrawingCanvas;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border DraggableCircle;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas HueSelectorCanvas;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle DraggableBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RGBR_TextBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RGBG_TextBox;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RGBB_TextBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HSVH_TextBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HSVS_TextBox;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HSVV_TextBox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HEX_TextBox;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\Views\LedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle ColorDisplay;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AxisConfigurator;component/views/ledview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\LedView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DrawingCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 17 "..\..\..\..\Views\LedView.xaml"
            this.DrawingCanvas.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ColorSelector_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\Views\LedView.xaml"
            this.DrawingCanvas.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ColorSelector_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\Views\LedView.xaml"
            this.DrawingCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.ColorSelector_MouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DraggableCircle = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.HueSelectorCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 22 "..\..\..\..\Views\LedView.xaml"
            this.HueSelectorCanvas.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.HueSelector_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\..\Views\LedView.xaml"
            this.HueSelectorCanvas.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.HueSelector_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\..\Views\LedView.xaml"
            this.HueSelectorCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.HueSelector_MouseMove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DraggableBox = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 5:
            this.RGBR_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.RGBG_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.RGBB_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.HSVH_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.HSVS_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.HSVV_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.HEX_TextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.ColorDisplay = ((System.Windows.Shapes.Rectangle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


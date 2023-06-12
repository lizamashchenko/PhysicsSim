﻿#pragma checksum "..\..\..\OpticsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BA61F2B3CC5B84AC0BE713EF63BF888F38DCEBD2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PhysicsSim;
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


namespace PhysicsSim {
    
    
    /// <summary>
    /// OpticsWindow
    /// </summary>
    public partial class OpticsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\OpticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox RaysCheckBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\OpticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox LensSelector;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\OpticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ObjectSelector;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\OpticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider FocusSlider;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\OpticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider DistanceSlider;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\OpticsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas field;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PhysicsSim;component/opticswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\OpticsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RaysCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 29 "..\..\..\OpticsWindow.xaml"
            this.RaysCheckBox.Checked += new System.Windows.RoutedEventHandler(this.ToggleButton_OnChecked);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\OpticsWindow.xaml"
            this.RaysCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.ToggleButton_OnUnchecked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LensSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\OpticsWindow.xaml"
            this.LensSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LensSelector_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ObjectSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 39 "..\..\..\OpticsWindow.xaml"
            this.ObjectSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ObjectSelector_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.FocusSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 46 "..\..\..\OpticsWindow.xaml"
            this.FocusSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.FocusSlider_OnValueChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DistanceSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 51 "..\..\..\OpticsWindow.xaml"
            this.DistanceSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.DiameterSlider_OnValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.field = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


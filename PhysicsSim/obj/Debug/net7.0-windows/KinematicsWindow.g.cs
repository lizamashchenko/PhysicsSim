﻿#pragma checksum "..\..\..\KinematicsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4B56CB0699D920151BE123D42AA738C9D627CAD1"
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
    /// KinematicsWindow
    /// </summary>
    public partial class KinematicsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SpeedTextBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SpeedSlider;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox airResistanceCheck;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ObjectSelector;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AngleTextBox;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider AngleSlider;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas field;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Himars;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\KinematicsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image himarsLauncher;
        
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
            System.Uri resourceLocater = new System.Uri("/PhysicsSim;component/kinematicswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\KinematicsWindow.xaml"
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
            this.SpeedTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\..\KinematicsWindow.xaml"
            this.SpeedTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SpeedSlider_OnValueChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SpeedSlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 3:
            this.airResistanceCheck = ((System.Windows.Controls.CheckBox)(target));
            
            #line 35 "..\..\..\KinematicsWindow.xaml"
            this.airResistanceCheck.Checked += new System.Windows.RoutedEventHandler(this.ToggleButton_OnChecked);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\KinematicsWindow.xaml"
            this.airResistanceCheck.Unchecked += new System.Windows.RoutedEventHandler(this.ToggleButton_OnUnchecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ObjectSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 39 "..\..\..\KinematicsWindow.xaml"
            this.ObjectSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ObjectSelector_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 43 "..\..\..\KinematicsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 52 "..\..\..\KinematicsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FireButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AngleTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 65 "..\..\..\KinematicsWindow.xaml"
            this.AngleTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.AngleTextBox_OnTextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.AngleSlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 9:
            this.field = ((System.Windows.Controls.Canvas)(target));
            return;
            case 10:
            this.Himars = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.himarsLauncher = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


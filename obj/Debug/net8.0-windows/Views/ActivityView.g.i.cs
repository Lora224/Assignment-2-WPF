﻿#pragma checksum "..\..\..\..\Views\ActivityView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CA8C37288031EFFCFFB104501B48A4172E921964"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Assignment_2_WPF.ViewModels;
using Assignment_2_WPF.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Assignment_2_WPF.Views {
    
    
    /// <summary>
    /// ActivityView
    /// </summary>
    public partial class ActivityView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 75 "..\..\..\..\Views\ActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar Calendar;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\Views\ActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddNewActivity;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Views\ActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveActivity;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\Views\ActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ActivityDetails;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\Views\ActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ActivityProgress;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Assignment 2 WPF;V1.0.0.0;component/views/activityview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ActivityView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Calendar = ((System.Windows.Controls.Calendar)(target));
            
            #line 77 "..\..\..\..\Views\ActivityView.xaml"
            this.Calendar.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Calendar_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AddNewActivity = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\..\Views\ActivityView.xaml"
            this.AddNewActivity.Click += new System.Windows.RoutedEventHandler(this.AddNewActivity_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RemoveActivity = ((System.Windows.Controls.Button)(target));
            
            #line 91 "..\..\..\..\Views\ActivityView.xaml"
            this.RemoveActivity.Click += new System.Windows.RoutedEventHandler(this.RemoveActivity_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ActivityDetails = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\..\Views\ActivityView.xaml"
            this.ActivityDetails.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 130 "..\..\..\..\Views\ActivityView.xaml"
            ((System.Windows.Controls.DataGrid)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ActivityProgress = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


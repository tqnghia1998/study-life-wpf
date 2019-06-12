﻿#pragma checksum "..\..\..\Subject\PageSubject.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FC3D8B9083937B6FBF805135D04CE58878B52605"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Client.Subject;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using System.Windows.Shell;
using WPFTextBoxAutoComplete;


namespace Client.Subject {
    
    
    /// <summary>
    /// PageSubject
    /// </summary>
    public partial class PageSubject : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 71 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listSubject;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn nameSubject;
        
        #line default
        #line hidden
        
        
        #line 211 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editTeacherName;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editTermIndex;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editTermYear;
        
        #line default
        #line hidden
        
        
        #line 247 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editFaculty;
        
        #line default
        #line hidden
        
        
        #line 260 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddSubject;
        
        #line default
        #line hidden
        
        
        #line 275 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdateSubject;
        
        #line default
        #line hidden
        
        
        #line 291 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSeeStatistic;
        
        #line default
        #line hidden
        
        
        #line 295 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchBar;
        
        #line default
        #line hidden
        
        
        #line 318 "..\..\..\Subject\PageSubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressBar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Client;component/subject/pagesubject.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Subject\PageSubject.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\Subject\PageSubject.xaml"
            ((Client.Subject.PageSubject)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Page_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.listSubject = ((System.Windows.Controls.ListView)(target));
            
            #line 78 "..\..\..\Subject\PageSubject.xaml"
            this.listSubject.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListSubject_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.nameSubject = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 4:
            this.editTeacherName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.editTermIndex = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.editTermYear = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.editFaculty = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btnAddSubject = ((System.Windows.Controls.Button)(target));
            
            #line 261 "..\..\..\Subject\PageSubject.xaml"
            this.btnAddSubject.Click += new System.Windows.RoutedEventHandler(this.BtnAddSubject_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnUpdateSubject = ((System.Windows.Controls.Button)(target));
            
            #line 276 "..\..\..\Subject\PageSubject.xaml"
            this.btnUpdateSubject.Click += new System.Windows.RoutedEventHandler(this.BtnUpdateSubject_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnSeeStatistic = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.searchBar = ((System.Windows.Controls.TextBox)(target));
            
            #line 296 "..\..\..\Subject\PageSubject.xaml"
            this.searchBar.KeyDown += new System.Windows.Input.KeyEventHandler(this.SearchBar_KeyDown);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ProgressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


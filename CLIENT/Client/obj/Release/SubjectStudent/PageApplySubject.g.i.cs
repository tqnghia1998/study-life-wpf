﻿#pragma checksum "..\..\..\SubjectStudent\PageApplySubject.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EBA04B93F683D3743455DB4E4C2828765B16AAB2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Client.SubjectStudent {
    
    
    /// <summary>
    /// PageApplySubject
    /// </summary>
    public partial class PageApplySubject : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 65 "..\..\..\SubjectStudent\PageApplySubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label totalcredit;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\SubjectStudent\PageApplySubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox termyear;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\SubjectStudent\PageApplySubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox termindex;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\SubjectStudent\PageApplySubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchKey;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\..\SubjectStudent\PageApplySubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox typeList;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\SubjectStudent\PageApplySubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView spListSubject;
        
        #line default
        #line hidden
        
        
        #line 323 "..\..\..\SubjectStudent\PageApplySubject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView spListRegisted;
        
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
            System.Uri resourceLocater = new System.Uri("/Client;component/subjectstudent/pageapplysubject.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SubjectStudent\PageApplySubject.xaml"
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
            this.totalcredit = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.termyear = ((System.Windows.Controls.ComboBox)(target));
            
            #line 104 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.termyear.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Termyear_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 105 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.termyear.DropDownOpened += new System.EventHandler(this.Combobox_DropDownOpened);
            
            #line default
            #line hidden
            
            #line 106 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.termyear.DropDownClosed += new System.EventHandler(this.Combobox_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.termindex = ((System.Windows.Controls.ComboBox)(target));
            
            #line 130 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.termindex.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Termindex_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 131 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.termindex.DropDownOpened += new System.EventHandler(this.Combobox_DropDownOpened);
            
            #line default
            #line hidden
            
            #line 132 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.termindex.DropDownClosed += new System.EventHandler(this.Combobox_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 4:
            this.searchKey = ((System.Windows.Controls.TextBox)(target));
            
            #line 151 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.searchKey.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchKey_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.typeList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 184 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.typeList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TypeList_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 185 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.typeList.DropDownOpened += new System.EventHandler(this.Combobox_DropDownOpened);
            
            #line default
            #line hidden
            
            #line 186 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            this.typeList.DropDownClosed += new System.EventHandler(this.Combobox_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 6:
            this.spListSubject = ((System.Windows.Controls.ListView)(target));
            return;
            case 9:
            this.spListRegisted = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 7:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.PreviewMouseLeftButtonDownEvent;
            
            #line 206 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 8:
            
            #line 306 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Registsubject_Click);
            
            #line default
            #line hidden
            break;
            case 10:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.PreviewMouseLeftButtonDownEvent;
            
            #line 333 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 11:
            
            #line 432 "..\..\..\SubjectStudent\PageApplySubject.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Deregistsubject_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

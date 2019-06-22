﻿#pragma checksum "..\..\..\UserInfo\PageUserInfo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3C209DCDF5F222BF5590BE747AADBEF3DEC28C3D"
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


namespace Client.UserInfo {
    
    
    /// <summary>
    /// PageUserInfo
    /// </summary>
    public partial class PageUserInfo : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 82 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editUsername;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox editPassword;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox editPassword2;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editEmail;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editFirstname;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editLastname;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox editFaculty;
        
        #line default
        #line hidden
        
        
        #line 219 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAvatar;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\..\UserInfo\PageUserInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
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
            System.Uri resourceLocater = new System.Uri("/Client;component/userinfo/pageuserinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserInfo\PageUserInfo.xaml"
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
            this.editUsername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.editPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.editPassword2 = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.editEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.editFirstname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.editLastname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.editFaculty = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btnAvatar = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 234 "..\..\..\UserInfo\PageUserInfo.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.BtnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


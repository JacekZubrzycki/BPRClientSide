using BPRMobileApp.ViewModels;
using BPRMobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BPRMobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(StudentHomeView), typeof(StudentHomeView));
            Routing.RegisterRoute(nameof(TeacherHomeView), typeof(TeacherHomeView));
            Routing.RegisterRoute(nameof(DetailedOfferView), typeof(DetailedOfferView));
            Routing.RegisterRoute(nameof(AddNewOfferView), typeof(AddNewOfferView));
        }

    }
}

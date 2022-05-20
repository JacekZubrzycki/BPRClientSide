using BPRMobileApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BPRMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        LoginViewModel viewModel;
        public LoginView()
        {
            InitializeComponent();
            viewModel = new LoginViewModel();
            this.BindingContext = viewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void UsernameEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.Username = String.Empty;
        }

        private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.Password = String.Empty;
        }
    }
}

using BPRMobileApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BPRMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private RegisterViewModel viewModel;
        public RegisterPage()
        {
            InitializeComponent();
            viewModel = new RegisterViewModel();
            this.BindingContext = viewModel;
        }

        private void UsernameEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.Username = String.Empty;
        }

        private void EmailEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.Email = String.Empty;
        }

        private void FirstNameEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.FirstName = String.Empty;
        }

        private void MiddleNameEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.MiddleName = String.Empty;
        }

        private void LastNameEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.LastName = String.Empty;
        }

        private void CityEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.City = String.Empty;
        }

        private void PhoneNumberEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.PhoneNumber = String.Empty;
        }

        private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            viewModel.Password = String.Empty;
        }
    }
}

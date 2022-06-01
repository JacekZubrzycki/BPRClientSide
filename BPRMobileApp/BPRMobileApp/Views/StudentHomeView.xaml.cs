using BPRMobileApp.Models;
using BPRMobileApp.Models.Responses;
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
    public partial class StudentHomeView : ContentPage
    {
        StudentHomeViewModel viewModel;
        public StudentHomeView()
        {
            InitializeComponent();
            viewModel = new StudentHomeViewModel();
            this.BindingContext = viewModel;
            
        }

        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            viewModel.OfferSelected((OfferDTOResponse)this.List.SelectedItem);
        }
    }
}

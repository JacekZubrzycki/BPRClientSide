using BPRMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BPRMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewOfferView : ContentPage
    {
        AddNewOfferViewModel viewModel;
        public AddNewOfferView()
        {
            InitializeComponent();
            viewModel = new AddNewOfferViewModel();
            this.BindingContext = viewModel;
        }
    }
}
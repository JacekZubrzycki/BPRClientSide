using BPRMobileApp.Models;
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
    public partial class TakeTestView : ContentPage
    {
        TakeTestViewModel viewModel;

        public TakeTestView()
        {
            InitializeComponent();
            viewModel = new TakeTestViewModel();
            BindingContext = viewModel;
        }
    }
}
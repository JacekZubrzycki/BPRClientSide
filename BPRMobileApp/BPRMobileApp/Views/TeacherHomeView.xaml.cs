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
    public partial class TeacherHomeView : ContentPage
    {
        TeacherHomeViewModel viewModel;
        public TeacherHomeView()
        {
            InitializeComponent();
            viewModel = new TeacherHomeViewModel();
            this.BindingContext = viewModel;
        }
    }
}

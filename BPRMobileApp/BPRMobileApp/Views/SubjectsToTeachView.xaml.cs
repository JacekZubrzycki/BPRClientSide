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
    public partial class SubjectsToTeachView : ContentPage
    {
        SubjectsToTeachViewModel viewModel;

        public SubjectsToTeachView()
        {
            InitializeComponent();
            viewModel = new SubjectsToTeachViewModel();
            this.BindingContext = viewModel;
        }

        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            viewModel.AddSubjectPopUpIsVisible = true;
        }
    }
}
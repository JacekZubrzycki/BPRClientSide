using BPRMobileApp.Models;
using BPRMobileApp.Models.Responses;
using BPRMobileApp.Services;
using BPRMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BPRMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedOfferView : ContentPage , IQueryAttributable
    {
        private DetailedOfferViewModel viewModel;
        private MobileSerializer serializer = new MobileSerializer();
        private OfferDTOResponse offer;
        public DetailedOfferView()
        {
            InitializeComponent();
            viewModel = new DetailedOfferViewModel();
            this.BindingContext = viewModel;
        }
        public string param { get; set; }
        public OfferDTOResponse Offer
        {
            get { return offer; }
            set
            {
                offer = value;
                viewModel.Offer = value;
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {      
            param = HttpUtility.UrlDecode(query["offer"]);
            Offer = serializer.DeserializeQueryToOffer(param);
        }
    }
}
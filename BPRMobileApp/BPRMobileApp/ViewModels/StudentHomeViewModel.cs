using Android.Graphics;
using BPRMobileApp.Models;
using BPRMobileApp.Services;
using BPRMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BPRMobileApp.ViewModels
{
    public class StudentHomeViewModel : BaseViewModel
    {
        #region Members

        MobileDataProvider provider = new MobileDataProvider();
        MobileSerializer serializer = new MobileSerializer();
        string token;

        private string searchBar;
        private Command searchBarCommand;
        private Offer selectedOffer;

        public Offer SelecetedOffer
        {
            get { return selectedOffer; }
            set { selectedOffer = value; }
        }

        //private string subjectName;
        //private string location;
        //private string price;
        private ObservableCollection<Offer> offers = new ObservableCollection<Offer>();


        public ObservableCollection<Offer> Offers
        {
            get { return offers; }
            set
            {
                offers = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties

        public Command SearchBarCommand
        {
            get { return searchBarCommand; }
            set { searchBarCommand = value; }
        }

        public string SearchBar
        {
            get { return searchBar; }
            set 
            { 
                searchBar = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region .Contr

        public StudentHomeViewModel()
        {
            GetToken();
            SearchBarCommand = new Command(OnSearchCliecked);
        }

        #endregion

        #region Methods

        public async void GetToken()
        {
            var a = await SecureStorage.GetAsync("token");
            token = a;
        }

        private async void GetOffersBySubjectAsync()
        {
            HttpResponseMessage response = await provider.GetOffersWithSubject(token, SearchBar);
            Offers =  new ObservableCollection<Offer>(await serializer.DeserializeToOffer(response));
        }

        private void OnSearchCliecked(Object obj)
        {
            GetOffersBySubjectAsync();
        }

        public async void OfferSelected(Offer offer)
        {
            foreach(Offer item in Offers)
            {
                if (item.Teacher.Name == offer.Teacher.Name && item.Price == offer.Price && item.Teacher.TeacherLocation == offer.Teacher.TeacherLocation)
                {
                    SelecetedOffer = item;
                    break;
                }
            }
            string offerToSend = serializer.SerializeOfferToQuerry(SelecetedOffer);
            await Shell.Current.GoToAsync($"//{nameof(DetailedOfferView)}?offer={offerToSend}");
        }

        #endregion
    }
}

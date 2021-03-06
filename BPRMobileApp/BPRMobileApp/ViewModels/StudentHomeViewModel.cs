using Android.Graphics;
using BPRMobileApp.Models;
using BPRMobileApp.Models.Responses;
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
        private OfferDTOResponse selectedOffer;
        private bool noOffersIsVisible;

        public bool NoOffersIsVisible
        {
            get { return noOffersIsVisible; }
            set 
            { 
                noOffersIsVisible = value;
                OnPropertyChanged();
            }
        }

        public OfferDTOResponse SelecetedOffer
        {
            get { return selectedOffer; }
            set { selectedOffer = value; }
        }

        //private string subjectName;
        //private string location;
        //private string price;
        private ObservableCollection<OfferDTOResponse> offers = new ObservableCollection<OfferDTOResponse>();


        public ObservableCollection<OfferDTOResponse> Offers
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
            NoOffersIsVisible = true;
            SubjectDTO subject = new SubjectDTO();
            subject.subject_Id = 1;
            subject.name = "Math";
            TeacherDTOResponse teacher = new TeacherDTOResponse("1", "jacek", "horsens", "11111", "email", 10);
            DateTime datefrom = new DateTime();
            DateTime dateto = new DateTime();
            OfferDTOResponse offer = new OfferDTOResponse(1, subject, teacher, 10, 10, datefrom, dateto);
            Offers.Add(offer);
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
            if (!(String.IsNullOrEmpty(SearchBar)))
            {
                HttpResponseMessage response = await provider.GetOffersWithSubject(token, SearchBar);
                Offers = new ObservableCollection<OfferDTOResponse>(await serializer.DeserializeToOffer(response));
                NoOffersIsVisible = Offers.Count < 0 ? true : false;
            }    
        }

        private void OnSearchCliecked(Object obj)
        {
            GetOffersBySubjectAsync();
        }

        public async void OfferSelected(OfferDTOResponse offer)
        {
            foreach(OfferDTOResponse item in Offers)
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

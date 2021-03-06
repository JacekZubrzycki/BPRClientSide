using BPRMobileApp.Models;
using BPRMobileApp.Models.Requests;
using BPRMobileApp.Models.Responses;
using BPRMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BPRMobileApp.ViewModels
{
    public class DetailedOfferViewModel : BaseViewModel
    {
        private OfferDTOResponse offer;
        private string offerId;
        private string subjectName;
        private string teacherName;
        private string location;
        private string phoneNumber;
        private string avgRating;
        private string price;
        private string from;
        private string to;
        private ObservableCollection<DateTime> fromList;
        private ObservableCollection<DateTime> toList;
        private int selectedIndexFromList;
        private int selectedIndexToList;
        private Command bookOffer;
        private MobileDataProvider provider;
        private MobileSerializer serializer = new MobileSerializer();
        private string token;
        List<OfferDTOResponse> offerts = new List<OfferDTOResponse>();



        public Command BookOffer
        {
            get { return bookOffer; }
            set
            {
                bookOffer = value;
            }
        }

        public int SelectedIndexFromList
        {
            get { return selectedIndexFromList; }
            set { selectedIndexFromList = value; }
        }
        
        public int SelectedIndexToList
        {
            get { return selectedIndexToList; }
            set { selectedIndexToList = value; }
        }

        public Command TestButton { get; set; } 

        public ObservableCollection<DateTime> ToList
        {
            get { return toList; }
            set
            {
                toList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DateTime> FromList
        {
            get { return fromList; }
            set
            {
                fromList = value;
                OnPropertyChanged();
            }
        }

        public string To
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged();
            }
        }

        public string From
        {
            get { return from; }
            set
            {
                from = value;
                OnPropertyChanged();
            }
        }

        public string Price
        {
            get { return price; }
            set 
            { 
                price = value;
                OnPropertyChanged();
            }
        }

        public string AvgRating
        {
            get { return avgRating; }
            set 
            { 
                avgRating = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set 
            { 
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get { return location; }
            set 
            { 
                location = value;
                OnPropertyChanged();
            }
        }

        public string TeacherName
        {
            get { return teacherName; }
            set 
            { 
                teacherName = value;
                OnPropertyChanged();
            }
        }

        public string SubjectName
        {
            get { return subjectName; }
            set 
            { 
                subjectName = value;
                OnPropertyChanged();
            }
        }

        public string OfferId
        {
            get { return offerId; }
            set 
            { 
                offerId = value;
                OnPropertyChanged();
            }
        }
        
        public OfferDTOResponse Offer
        {
            get { return offer; }
            set 
            { 
                offer = value;
                OfferId = Offer.Offer_Id.ToString();
                SubjectName = Offer.Subjects.name;
                TeacherName = Offer.Teacher.Name;
                Location = Offer.Teacher.TeacherLocation;
                PhoneNumber = Offer.Teacher.PhoneNumber;
                AvgRating = Offer.Teacher.AvgRating.ToString();
                Price = Offer.Price.ToString();
                From = Offer.From.ToUniversalTime().ToString();
                To = Offer.To.ToUniversalTime().ToString();
            }
        }

        public DetailedOfferViewModel()
        {
            TestButton = new Command(PopulateFromList);
            FromList = new ObservableCollection<DateTime>();
            ToList = new ObservableCollection<DateTime>();
            BookOffer = new Command(BookAnOffer);
            provider = new MobileDataProvider();
            GetBookedTimes();
        }

        private void PopulateFromList()
        {

            GetBookedTimes();

            
        }

        private async void BookAnOffer()
        {
            GetToken();
            BookTimeDTO bookOffer = new BookTimeDTO(Offer.Offer_Id,
                FromList.ElementAt(SelectedIndexFromList).ToUniversalTime(), ToList.ElementAt(SelectedIndexToList).ToUniversalTime());
            HttpResponseMessage response = await provider.BookAnOffer(token, bookOffer);
            Console.WriteLine(response);
        }

        public async void GetToken()
        {
            var a = await SecureStorage.GetAsync("token");
            token = a;
        }

        public async void GetBookedTimes()
        {
            GetToken();
            HttpResponseMessage response = await provider.GetBookedTiemsByOfferId(token, Offer.Offer_Id);
            offerts = await serializer.DeserializeToOffer(response);
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            TimeSpan amountOfHours = new TimeSpan();
            DateTime date = new DateTime();
            Int32 amountOfMinutes;
            fromDate = Convert.ToDateTime(From);
            toDate = Convert.ToDateTime(To);
            amountOfHours = toDate.Subtract(fromDate);
            amountOfMinutes = (int)amountOfHours.TotalMinutes;
            int timeToBeAdded = Offer.MinTime;

            for (double i = 0; i < amountOfMinutes; i += timeToBeAdded)
            {
                date = fromDate.AddMinutes(i);
                FromList.Add(date);
            }

            foreach (OfferDTOResponse offer in offerts)
            {

                if (FromList.Contains(Convert.ToDateTime(offer.From)))
                {
                    FromList.Remove(Convert.ToDateTime(offer.From));
                }

                if (FromList.Contains(Convert.ToDateTime(offer.To)))
                {
                    FromList.Remove(Convert.ToDateTime(offer.To));
                }
            }

            ToList = new ObservableCollection<DateTime>(FromList);
            if (ToList.Count > 0)
            {
                ToList.RemoveAt(0);
            }
            
        }

    }
}

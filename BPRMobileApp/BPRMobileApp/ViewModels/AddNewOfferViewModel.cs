using Android.Widget;
using BPRMobileApp.Models;
using BPRMobileApp.Models.Requests;
using BPRMobileApp.Services;
using BPRMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BPRMobileApp.ViewModels
{
    public class AddNewOfferViewModel : BaseViewModel
    {
        #region Members

        private Command addNewOffer;
        private ObservableCollection<string> subjects;
        private string price;
        private string minTime;
        private DateTime fromDate;
        private DateTime toDate;
        private TimeSpan fromTime;
        private TimeSpan toTime;
        MobileDataProvider provider = new MobileDataProvider();
        MobileSerializer serializer = new MobileSerializer();
        private List<SubjectDTO> dataBaseSubjects;
        string token;
        HttpResponseMessage response;

        #endregion

        #region Properties

        public TimeSpan ToTime
        {
            get { return toTime; }
            set 
            { 
                toTime = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan FromTime
        {
            get { return fromTime; }
            set 
            { 
                fromTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime ToDate
        {
            get { return toDate; }
            set 
            { 
                toDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime FromDate
        {
            get { return fromDate; }
            set 
            { 
                fromDate = value;
                OnPropertyChanged();
            }
        }

        public string MinTime
        {
            get { return minTime; }
            set 
            { 
                minTime = value;
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

        public ObservableCollection<string> Subjects
        {
            get { return subjects; }
            set 
            { 
                subjects = value;
                OnPropertyChanged();
            }
        }

        public Command AddNewOffer
        {
            get { return addNewOffer; }
            set { addNewOffer = value; }
        }

        #endregion

        #region .Constr

        public AddNewOfferViewModel()
        {
            AddNewOffer = new Command(AddNewOfferClicked);
            dataBaseSubjects = new List<SubjectDTO>();
            Subjects = new ObservableCollection<string>();
            GetAllSubjects();
        }

        #endregion

        #region Methods

        private async void AddNewOfferClicked()
        {
            DateTime datef = new DateTime();
            DateTime datet = new DateTime();
            datef = FromDate.Add(FromTime);
            datet = ToDate.Add(ToTime);

            SubjectDTO subject = new SubjectDTO();
            foreach(SubjectDTO s in dataBaseSubjects)
            {
                if (Subjects.Contains(s.name))
                {
                    subject = s;
                    break;
                }
            }

            if (subject != null && 
                Price != null && 
                MinTime != null && 
                datef != null && 
                datet != null)
            {
                OfferDTORequest offer = new OfferDTORequest(subject.subject_Id,
                    Decimal.Parse(Price), Int32.Parse(MinTime),
                    datef.ToUniversalTime(), datet.ToUniversalTime());
                response = await provider.PostAnOffer(token, offer);
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, "Full fill all required data", ToastLength.Short).Show();
            }

            if (response.IsSuccessStatusCode)
            {
                Toast.MakeText(Android.App.Application.Context, "Offer has been created", ToastLength.Short).Show();
                await Shell.Current.GoToAsync($"//{nameof(TeacherHomeView)}");
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, "Something went wrong\nTry again", ToastLength.Short).Show();
            }
            
        }

        public async void GetAllSubjects()
        {
            GetToken();
            HttpResponseMessage response = await provider.GetSubjects(token);
            dataBaseSubjects = await serializer.DeserializeToDataBaseSubject(response);
            foreach (SubjectDTO subject in dataBaseSubjects)
            {
                Subjects.Add(subject.name);
            }
        }

        public async void GetToken()
        {
            token = await SecureStorage.GetAsync("token");
        }
        
        #endregion
    }
}

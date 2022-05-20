using BPRMobileApp.Models;
using BPRMobileApp.Services;
using BPRMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BPRMobileApp.ViewModels
{
    public class AddNewOfferViewModel : BaseViewModel
    {
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
        private List<DataBaseSubject> dataBaseSubjects;
        string token;

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

        public AddNewOfferViewModel()
        {
            AddNewOffer = new Command(AddNewOfferClicked);
            dataBaseSubjects = new List<DataBaseSubject>();
            Subjects = new ObservableCollection<string>();
            GetAllSubjects();
        }

        private async void AddNewOfferClicked()
        {
            DateTime datef = new DateTime();
            DateTime datet = new DateTime();
            datef = FromDate.Add(FromTime);
            datet = ToDate.Add(ToTime);

            DataBaseSubject subject = new DataBaseSubject();
            foreach(DataBaseSubject s in dataBaseSubjects)
            {
                if (Subjects.Contains(s.name))
                {
                    subject = s;
                    break;
                }
            }
            CreateOffer offer = new CreateOffer(subject.subject_Id, Price, MinTime,
                datef.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK"),
                datet.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK"));
            HttpResponseMessage response = await provider.PostAnOffer(token, offer);
        }

        public async void GetAllSubjects()
        {
            GetToken();
            HttpResponseMessage response = await provider.GetSubjects(token);
            dataBaseSubjects = await serializer.DeserializeToDataBaseSubject(response);
            foreach (DataBaseSubject subject in dataBaseSubjects)
            {
                Subjects.Add(subject.name);
            }
        }

        public async void GetToken()
        {
            var a = await SecureStorage.GetAsync("token");
            token = a;
        }
    }
}

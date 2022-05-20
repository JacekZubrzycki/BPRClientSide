using Android.Content;
using BPRMobileApp.Models;
using BPRMobileApp.Services;
using BPRMobileApp.Views;
using Newtonsoft.Json;
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
    public class TeacherHomeViewModel : BaseViewModel
    {
        string token;
        MobileDataProvider provider = new MobileDataProvider();
        MobileSerializer serializer = new MobileSerializer();
        private List<DataBaseSubject> dataBaseSubjects;
        private ObservableCollection<string> subjects;
        private Command addNewOffer;

        public Command AddNewOffer
        {
            get { return addNewOffer; }
            set { addNewOffer = value; }
        }

        public ObservableCollection<string> Subjects
        {
            get  { return subjects; }
            set  
            { 
                subjects = value;
                OnPropertyChanged();
            }
        }
        public TeacherHomeViewModel()
        {
            Subjects = new ObservableCollection<string>();
            dataBaseSubjects = new List<DataBaseSubject>();
            AddNewOffer = new Command(AddNewOfferClicked);

        }

        public async void GetToken()
        {
            var a = await SecureStorage.GetAsync("token");
            token = a;
        }

        public async void AddSubject()
        {
            Subject subject = new Subject();
            subject.name = "TestAddSubject";          
            HttpResponseMessage response = await provider.AddSubject(subject, token);
            Console.WriteLine(response.Content);
        }

        public async void GetAllSubjects()
        {
            HttpResponseMessage response = await provider.GetSubjects(token);
            dataBaseSubjects = await serializer.DeserializeToDataBaseSubject(response);
            foreach (DataBaseSubject subject in dataBaseSubjects)
            {
                Subjects.Add(subject.name);
            }
        }

        public async void PostSubjectOffer()
        {
            //Offer offer = new Offer();
            //offer.price = "1";
            //offer.minTime = "1";
            //offer.subject_Id = "1";
            //HttpResponseMessage response = await provider.PostAnOffer(offer, token);
        }

        private async void AddNewOfferClicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(AddNewOfferView)}");
        }
    }
}

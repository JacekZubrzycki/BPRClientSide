using BPRMobileApp.Models;
using BPRMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace BPRMobileApp.ViewModels
{
    public class SubjectsToTeachViewModel : BaseViewModel
    {
        private string token;
        private List<SubjectDTO> dataBaseSubjects;
        private ObservableCollection<string> subjects;
        MobileDataProvider provider = new MobileDataProvider();
        MobileSerializer serializer = new MobileSerializer();
        private int selectedIndex;
        private bool addSubjectPopUpIsVisible;

        public int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; OnPropertyChanged(); } }
        public bool AddSubjectPopUpIsVisible { get { return addSubjectPopUpIsVisible; } set { addSubjectPopUpIsVisible = value; OnPropertyChanged(); } }

        public ObservableCollection<string> Subjects
        {
            get { return subjects; }
            set
            {
                subjects = value;
                OnPropertyChanged();
            }
        }

        public SubjectsToTeachViewModel()
        {
            Subjects = new ObservableCollection<string>();
            dataBaseSubjects = new List<SubjectDTO>();
            AddSubjectPopUpIsVisible = false;
        }

        public async void GetAllSubjects()
        {
            HttpResponseMessage response = await provider.GetSubjects(token);
            dataBaseSubjects = await serializer.DeserializeToDataBaseSubject(response);
            foreach (SubjectDTO subject in dataBaseSubjects)
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

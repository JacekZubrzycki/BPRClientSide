using BPRMobileApp.Models.Requests;
using BPRMobileApp.Models.Responses;
using BPRMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BPRMobileApp.ViewModels
{
    public class TakeTestViewModel : BaseViewModel
    {
        private MobileDataProvider provider;
        private string token;
        private string subject_id;
        private Command takeTestButtonClick;
        private MobileSerializer serializer;
        private ObservableCollection<QuestionsDTOResponse> questionnaire;
        private bool subjectDetailsPopUpIsVisible;
        private string option1, option2, option3, option4;
        private string nextButtonText;
        private string question;
        private int questionCounter;
        private Command nextButtonClicked;
        ObservableCollection<string> options;
        private List<QuestionAnswersDTO> answers;
        private string groupName;
        private bool option1Value;
        private bool option2Value;
        private bool option3Value;
        private bool option4Value;

        public bool Option1Value
        {
            get { return option1Value; }
            set 
            {
                option1Value = value;
                OnPropertyChanged();
            }
        }
        
        public bool Option2Value
        {
            get { return option2Value; }
            set 
            {
                option2Value = value;
                OnPropertyChanged();
            }
        }
        
        public bool Option3Value
        {
            get { return option3Value; }
            set 
            {
                option3Value = value;
                OnPropertyChanged();
            }
        }
        
        public bool Option4Value
        {
            get { return option4Value; }
            set 
            {
                option4Value = value;
                OnPropertyChanged();
            }
        }

        public string GroupName
        {
            get { return groupName; }
            set 
            { 
                groupName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Options
        {
            get { return options; }
            set
            {
                options = value;
                OnPropertyChanged();
            }
        }

        public Command NextButtonClicked
        {
            get { return nextButtonClicked; }
            set
            {
                nextButtonClicked = value;
                OnPropertyChanged();
            }
        }

        public string Question
        {
            get { return question; }
            set
            {
                question = value;
                OnPropertyChanged();
            }
        }

        public string NextButtonText
        {
            get { return nextButtonText; }
            set 
            { 
                nextButtonText = value;
                OnPropertyChanged();
            }
        }

        public string Option1
        {
            get { return option1; }
            set 
            { 
                option1 = value;
                OnPropertyChanged();
            }
        }

        public string Option2
        {
            get { return option2; }
            set 
            { 
                option2 = value;
                OnPropertyChanged();
            }
        }

        public string Option3
        {
            get { return option3; }
            set 
            { 
                option3 = value;
                OnPropertyChanged();
            }
        }

        public string Option4
        {
            get { return option4; }
            set 
            { 
                option4 = value;
                OnPropertyChanged();
            }
        }

        public bool SubjectDetailsPopUpIsVisible
        {
            get { return subjectDetailsPopUpIsVisible; }
            set 
            { 
                subjectDetailsPopUpIsVisible = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<QuestionsDTOResponse> Questionnaire
        {
            get { return questionnaire; }
            set 
            { 
                questionnaire = value;
                OnPropertyChanged();
            }
        }

        public Command TakeTestButtonClick
        {
            get { return takeTestButtonClick; }
            set { takeTestButtonClick = value; }
        }

        public TakeTestViewModel()
        {
            provider = new MobileDataProvider();
            serializer = new MobileSerializer();
            NextButtonClicked = new Command(SetOptions);
            SubjectDetailsPopUpIsVisible = false;
            GetTest();
            questionCounter = 0;
            options = new ObservableCollection<string>();
            NextButtonText = "Next";
            answers = new List<QuestionAnswersDTO>();
        }

        public async void GetToken()
        {
            var a = await SecureStorage.GetAsync("token");
            token = a;
        }

        private async void GetTest()
        {
           GetToken();
           HttpResponseMessage response = await provider.TakeTest(token, "1");
           Questionnaire =  new ObservableCollection<QuestionsDTOResponse>(await serializer.DeserializeToQuestionnaire(response));
        }

        public void SetOptions()
        {
            if (Option1Value)
            {
                QuestionAnswersDTO answer = new QuestionAnswersDTO(Int32.Parse(Questionnaire[questionCounter].Question_id), Questionnaire[questionCounter].Option[0].Question_option_id);
                answers.Add(answer);
            }

            else if (Option2Value)
            {

            }

            else if (Option3Value)
            {

            }

            else if (Option4Value)
            {

            }

            if (questionCounter < Questionnaire.Count)
            {
                Question = Questionnaire[questionCounter].Question;
                options.Clear();
                foreach (QuestionOptionDTOResponse model in Questionnaire[questionCounter].Option)
                {
                    options.Add(model.Option);
                }

                Option1 = options[0];
                //Option2 = options[1];
                //Option3 = options[2];
                //Option4 = options[3];
                questionCounter++;
                if (questionCounter == Questionnaire.Count)
                {
                    NextButtonText = "Submit";
                    provider.SubmitAnswers(token, answers);
                }
            }
            
        }
    }
}

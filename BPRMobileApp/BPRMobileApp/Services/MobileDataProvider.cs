using BPRMobileApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using BPRMobileApp.Models.Requests;
using BPRMobileApp.Models;

namespace BPRMobileApp.Services
{
    public class MobileDataProvider
    {
        

        #region Members

        private const string baseUrl = "https://private-teaching.azurewebsites.net/";
        private static HttpClient client;
        private HttpContent httpContent;
        private HttpResponseMessage response;
        private string json;
        private MobileSerializer serializer;

        #endregion

        #region Constructor

        public MobileDataProvider()
        {
            client = new HttpClient();
            serializer = new MobileSerializer(); 
        }

        #endregion

        #region Methods

        public async Task<HttpResponseMessage> RegisterUser(UserRegisterDTO user, bool teacherAccount)
        {
            if (teacherAccount)
            {
                return response = await client.PostAsync(baseUrl + "Authenticate/register-teacher", serializer.SerializeObject(user));   
            }
            else
            {
                return response = await client.PostAsync(baseUrl + "Authenticate/register-student", serializer.SerializeObject(user));
            }
            
        }

        public async Task<HttpResponseMessage> LoginUser(UserLoginDTO user)
        {
            return response = await client.PostAsync(baseUrl + "Authenticate/login", serializer.SerializeObject(user));
        }

        public async Task<HttpResponseMessage> AddSubject(int subject_Id, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + $"Teacher/AddSubjectToTeacher?subject_Id={subject_Id}");
        }

        public async Task<HttpResponseMessage> GetSubjects(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + "Teacher/GetAllSubjects");
        }

        public async Task<HttpResponseMessage> GetPostedSubjects(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + "Student/GetAllPostedOffers");
        }

        public async Task<HttpResponseMessage> PostAnOffer(string token, OfferDTORequest offer)
        {  
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.PostAsync(baseUrl + "Teacher/PostAnOffer", serializer.SerializeObject(offer));
        }

        public async Task<HttpResponseMessage> GetOffersWithSubject(string token, string subject)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + $"GetOffersAccordingToSubjectName?subject_name={subject}");
        }

        public async Task<HttpResponseMessage> BookAnOffer(string token, BookTimeDTO offer)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.PostAsync(baseUrl + "Student/BookAnOffer", serializer.SerializeToBookOffer(offer));
        }

        public async Task<HttpResponseMessage> GetBookedTiemsByOfferId(string token, int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + $"Student/GetBookedTimesOnlyAccordingToOfferID?offer_id={id}");
        }

        public async Task<HttpResponseMessage> TakeTest(string token, string subjectId)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + $"Teacher/TakeTest?subject_id={subjectId}");
        }

        public async Task<HttpResponseMessage> AddQuestionnaireToSubjects(string token, QuestionsDTOResponse questionnaire)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.PostAsync(baseUrl + $"Teacher/AddQuestionnaireToSubjects", serializer.SerializeObject(questionnaire));
        }
        
        public async Task<HttpResponseMessage> AddQuestionWithOptions(string token, QuestionOptionDTOResponse question)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.PostAsync(baseUrl + $"Teacher/AddNewQuestionWithOptions", serializer.SerializeObject(question));
        }
        
        public async Task<HttpResponseMessage> SubmitAnswers(string token, List<QuestionAnswersDTO> answers)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.PostAsync(baseUrl + $"Teacher/SubmitAnswers", serializer.SerializeObject(answers));
        }


        #endregion
    }
}

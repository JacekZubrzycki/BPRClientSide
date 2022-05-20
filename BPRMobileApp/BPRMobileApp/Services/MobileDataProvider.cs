using BPRMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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

        public async Task<HttpResponseMessage> RegisterUser(RegisterUser user, bool teacherAccount)
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

        public async Task<HttpResponseMessage> LoginUser(LoginUser user)
        {
            return response = await client.PostAsync(baseUrl + "Authenticate/login", serializer.SerializeObject(user));
        }

        public async Task<HttpResponseMessage> AddSubject(Subject subject, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.PostAsync(baseUrl + "ListSubjects/AddSubject", serializer.SerializeObject(subject));
        }

        public async Task<HttpResponseMessage> GetSubjects(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + "ListSubjects/GetSubjects");
        }

        public async Task<HttpResponseMessage> GetPostedSubjects(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + "ListSubjects/GetPostedSubjects");
        }

        public async Task<HttpResponseMessage> PostAnOffer(string token, CreateOffer offer)
        {  
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.PostAsync(baseUrl + "ListSubjects/PostAnOffer", serializer.SerializeObject(offer));
        }

        public async Task<HttpResponseMessage> GetPostedOffers(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + "ListSubject/GetPostedSubjects");
        }

        public async Task<HttpResponseMessage> GetOffersWithSubject(string token, string subject)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + $"ListSubjects/GetOffersWithSubject?name={subject}");
        }

        public async Task<HttpResponseMessage> BookAnOffer(string token, BookOffer offer)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.PostAsync(baseUrl + "Booking/BookAnOffer", serializer.SerializeToBookOffer(offer));
        }

        public async Task<HttpResponseMessage> GetBookedTiemsByOfferId(string token, int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return response = await client.GetAsync(baseUrl + $"Booking/GetBookedTimesSubjectId?id={id}");
        }

        #endregion
    }
}

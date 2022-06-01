using BPRMobileApp.Models;
using BPRMobileApp.Models.Requests;
using BPRMobileApp.Models.Responses;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BPRMobileApp.Services
{
    public class MobileSerializer
    {
        #region Members

        private string serializedJson;
        private string stream;
        private Task<string> deserializedJson; 
        private HttpContent httpContent;
        private Token token;
        private List<SubjectDTO> subjects;
        JwtSecurityTokenHandler jwtSecurityTokenHandler;
        SecurityToken securityToken;
        JwtSecurityToken jwtSecurityToken;
        IEnumerable<Claim> claims;
        private List<OfferDTOResponse> offers = new List<OfferDTOResponse>();

        #endregion

        #region Properties

        public List<OfferDTOResponse> Offers
        {
            get { return offers; }
            set { offers = value; }
        }

        public IEnumerable<Claim> Claims
        {
            get { return claims; }
            set { claims = value; }
        }

        public JwtSecurityToken JwtSecurityToken
        {
            get { return jwtSecurityToken; }
            set { jwtSecurityToken = value; }
        }

        public SecurityToken SecurityToken
        {
            get { return securityToken; }
            set { securityToken = value; }
        }

        public string Stream
        {
            get { return stream; }
            set { stream = value;}
        }
        public Task<string> DeserializedJson
        {
            get { return deserializedJson; }
            set { deserializedJson = value; }
        }
        public string SerializedJson
        {
            get { return serializedJson; }
            set { serializedJson = value; }
        }

        public HttpContent HttpContent
        {
            get { return httpContent; }
            set { httpContent = value; }
        }

        public Token JsonWebToken
        {
            get { return token; }
            set { token = value; }
        }

        public List<SubjectDTO> Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }

        public JwtSecurityTokenHandler JwtSecurityTokenHandler
        {
            get { return jwtSecurityTokenHandler; }
            set { jwtSecurityTokenHandler = value; }
        }

        #endregion

        #region .Constr

        public MobileSerializer()
        {
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        #endregion

        #region Methods

        public HttpContent SerializeObject(Object obj)
        {
            serializedJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
            serializedJson.Replace("\\", " ");
            return HttpContent = new StringContent(serializedJson, Encoding.UTF8, "application/json");
        }

        public async Task<Token> DeserialzieToToken(HttpResponseMessage response)
        {
            DeserializedJson = response.Content.ReadAsStringAsync();
            Stream = await DeserializedJson;
            return JsonWebToken = JsonConvert.DeserializeObject<Token>(stream);
        }

        public async Task<List<SubjectDTO>> DeserializeToDataBaseSubject(HttpResponseMessage response)
        {
            DeserializedJson = response.Content.ReadAsStringAsync();
            Stream = await DeserializedJson;
            return Subjects = JsonConvert.DeserializeObject<List<SubjectDTO>>(Stream);
        }

        public Task<IEnumerable<Claim>> DeserializeToken(Token token)
        {
            SecurityToken = JwtSecurityTokenHandler.ReadToken(token.token);
            JwtSecurityToken = SecurityToken as JwtSecurityToken;
            return Task.FromResult(Claims = JwtSecurityToken.Claims);
        }

        public async Task<List<OfferDTOResponse>> DeserializeToOffer(HttpResponseMessage response)
        {
            DeserializedJson = response.Content.ReadAsStringAsync();
            Stream = await DeserializedJson;
            return  JsonConvert.DeserializeObject<List<OfferDTOResponse>>(Stream);
        }

        public OfferDTOResponse DeserializeQueryToOffer(string json)
        {
            return JsonConvert.DeserializeObject<OfferDTOResponse>(json);
        }

        public string SerializeOfferToQuerry(OfferDTOResponse offer)
        {
            serializedJson = JsonConvert.SerializeObject(offer, Formatting.Indented);
            serializedJson.Replace("\\", " ");
            return serializedJson;
        }

        public HttpContent SerializeToBookOffer(BookTimeDTO offer)
        {
            serializedJson = JsonConvert.SerializeObject(offer, Formatting.Indented);
            return HttpContent = new StringContent(serializedJson, Encoding.UTF8, "application/json");
        }

        public async Task<ObservableCollection<QuestionsDTOResponse>> DeserializeToQuestionnaire(HttpResponseMessage response)
        {
            DeserializedJson = response.Content.ReadAsStringAsync();
            Stream = await DeserializedJson;
            return JsonConvert.DeserializeObject<ObservableCollection<QuestionsDTOResponse>>(Stream);
        }

        #endregion
    }
}

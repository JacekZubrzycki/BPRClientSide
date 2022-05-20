using Android.Content.Res;
using Android.Widget;
using BPRMobileApp.Models;
using BPRMobileApp.Services;
using BPRMobileApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BPRMobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Members

        private Command loginCommand;
        private Command registeCommand;
        private string username;
        private string password;
        private MobileDataProvider dataProvider;
        private MobileSerializer serializer;
        private Token token;
        JwtSecurityTokenHandler handler;
        IEnumerable<Claim> deserializedToken;
        List<string> claims;
        private const string teacher = "Teacher";
        private Color usernameFrameBackgroundColor;
        private Color passwordFrameBackgroundColor;
        private Command keyboardAppear;

        #endregion

        #region Properties

        public Command KeyboardAppear
        {
            get { return keyboardAppear; }
            set { keyboardAppear = value; }
        }

        public Color UsernameFrameBackgroundColor
        {
            get { return usernameFrameBackgroundColor; }
            set 
            {
                usernameFrameBackgroundColor = value;
                OnPropertyChanged();
            }
        }

        public Color PasswordFrameBackgroundColor
        {
            get { return passwordFrameBackgroundColor; }
            set 
            {
                passwordFrameBackgroundColor = value;
                OnPropertyChanged();
            }
        }

        public List<string> Claims
        {
            get { return claims; }
            set { claims = value; }
        }

        public IEnumerable<Claim> DeserializedToken
        {
            get { return deserializedToken; }
            set { deserializedToken = value; }
        }

        public JwtSecurityTokenHandler Handler
        {
            get { return handler; }
            set { handler = value; }
        }

        public MobileDataProvider DataProvider
        {
            get { return dataProvider; }
            set { dataProvider = value; }
        }

        public MobileSerializer Serializer
        {
            get { return serializer; }
            set { serializer = value; }
        }
        public Command LoginCommand
        {
            get { return loginCommand; }
            set { loginCommand = value; }
        }

        public Command RegisterCommand
        {
            get { return registeCommand; }
            set { registeCommand = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public Token Token
        {
            get { return token; }
            set { token = value; }
        }

        #endregion

        #region .Contr

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
            DataProvider = new MobileDataProvider();
            Serializer = new MobileSerializer();
            Handler = new JwtSecurityTokenHandler();
            Claims = new List<string>();
            UsernameFrameBackgroundColor = Color.FromHex("#9ca7b8");
            PasswordFrameBackgroundColor = Color.FromHex("#9ca7b8");
        }

        #endregion

        private async void OnLoginClicked(object obj)
        {
            if (String.IsNullOrEmpty(Username))
            {
                UsernameFrameBackgroundColor = Color.FromHex("#e31b1b");
                Username = "Username can not be empty";
                return;
            }
            else if (String.IsNullOrEmpty(Password))
            {
                PasswordFrameBackgroundColor = Color.FromHex("#e31b1b");
                Password = "Password can not be empty";
            }
            LoginUser user = new LoginUser(Username, Password);
            var response = await dataProvider.LoginUser(user);          

            if (response.IsSuccessStatusCode)
            {              
                Token = await serializer.DeserialzieToToken(response);
                await SecureStorage.SetAsync("token", Token.token);
                DeserializedToken = await serializer.DeserializeToken(Token);
                GetClaims();
                if (IsTeacherAccount())
                {
                    await Shell.Current.GoToAsync($"//{nameof(TeacherHomeView)}");  
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(StudentHomeView)}");
                }                
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, "Something went wrong", ToastLength.Short).Show();
            }          
        }

        private bool IsTeacherAccount()
        {
            if (Claims[3].ToString().Contains(teacher))
            {
                return true;
            }
            else
                return false;
        }

        private void GetClaims()
        {
            foreach (Claim claim in DeserializedToken)
            {
                Claims.Add(claim.ToString());
            }
        }

        private async void OnRegisterClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
    }
}

using Android.Widget;
using BPRMobileApp.Models;
using BPRMobileApp.Services;
using BPRMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace BPRMobileApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Const

        private Color emptyEntry = Color.FromHex("#e31b1b");
        private Color defautEntry = Color.FromHex("#ffffff");
        private Color defaultText = Color.FromHex("#000000");
        private string emptyEntryText = " can not be empty";

        #endregion

        #region Members

        private Command registerCommand;
        private MobileDataProvider dataProvider = new MobileDataProvider();
        private RegisterUser registerUser;
        private Color usernameFrameBackgroundColor;
        private Color passwordFrameBackgroundColor;
        private Color emailFrameBackgroundColor;
        private Color phoneNumberFrameBackgroundColor;
        private Color firstNameFrameBackgroundColor;
        private Color middleNameFrameBackgroundColor;
        private Color lastNameFrameBackgroundColor;
        private Color cityFrameBackgroundColor;
        private Color usernameTextColor;
        private Color passwordTextColor;
        private Color emailTextColor;
        private Color phoneNumberTextColor;
        private Color firstNameTextColor;
        private Color middleNameTextColor;
        private Color lastNameTextColor;
        private Color cityTextColor;
        private string username;
        private string password;
        private string email;
        private string phoneNumber;
        private string firstname;
        private string middleName;
        private string lastName;
        private string city;
        private bool teacherAccount;

        #endregion

        #region Properties

        public Command RegisterCommand
        {
            get { return registerCommand; }
            set { registerCommand = value; }
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
        
        public Color EmailFrameBackgroundColor
        {
            get { return emailFrameBackgroundColor; }
            set
            {
                emailFrameBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        
        public Color FirstNameFrameBackgroundColor
        {
            get { return firstNameFrameBackgroundColor; }
            set
            {
                firstNameFrameBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        
        public Color MiddleNameFrameBackgroundColor
        {
            get { return middleNameFrameBackgroundColor; }
            set
            {
                middleNameFrameBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        public Color LastNameFrameBackgroundColor
        {
            get { return lastNameFrameBackgroundColor; }
            set
            {
                lastNameFrameBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        public Color PhoneNumberFrameBackgroundColor
        {
            get { return phoneNumberFrameBackgroundColor; }
            set
            {
                phoneNumberFrameBackgroundColor = value;
                OnPropertyChanged();
            }
        }
        public Color CityFrameBackgroundColor
        {
            get { return cityFrameBackgroundColor; }
            set
            {
                cityFrameBackgroundColor = value;
                OnPropertyChanged();
            }
        }

        public Color UsernameTextColor
        {
            get { return usernameTextColor; }
            set
            {
                usernameTextColor = value;
                OnPropertyChanged();
            }
        }

        public Color EmailTextColor
        {
            get { return emailTextColor; }
            set
            {
                emailTextColor = value;
                OnPropertyChanged();
            }
        }

        public Color FirstNameTextColor
        {
            get { return firstNameTextColor; }
            set
            {
                firstNameTextColor = value;
                OnPropertyChanged();
            }
        }

        public Color MiddleNameTextColor
        {
            get { return middleNameTextColor; }
            set
            {
                middleNameTextColor = value;
                OnPropertyChanged();
            }
        }

        public Color LastNameTextColor
        {
            get { return lastNameTextColor; }
            set
            {
                lastNameTextColor = value;
                OnPropertyChanged();
            }
        }

        public Color CityTextColor
        {
            get { return cityTextColor; }
            set
            {
                cityTextColor = value;
                OnPropertyChanged();
            }
        }

        public Color PhoneNumberTextColor
        {
            get { return phoneNumberTextColor; }
            set
            {
                phoneNumberTextColor = value;
                OnPropertyChanged();
            }
        }

        public Color PasswordTextColor
        {
            get { return passwordTextColor; }
            set
            {
                passwordTextColor = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return username; }
            set 
            { 
                username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                OnPropertyChanged();
            }
        }
        public string FirstName
        {
            get { return firstname; }
            set 
            { 
                firstname = value;
                OnPropertyChanged();
            }
        }
        public string MiddleName
        {
            get { return middleName; }
            set 
            { 
                middleName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return lastName; }
            set 
            { 
                lastName = value;
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
        public string City
        {
            get { return city; }
            set 
            { 
                city = value;
                OnPropertyChanged();
            }
        }

        public bool TeacherAccount
        {
            get { return teacherAccount; }
            set 
            { 
                teacherAccount = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region .Contr

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
            TeacherAccount = false;
            UsernameFrameBackgroundColor = defautEntry;
            FirstNameFrameBackgroundColor = defautEntry;
            MiddleNameFrameBackgroundColor = defautEntry;
            LastNameFrameBackgroundColor = defautEntry;
            PhoneNumberFrameBackgroundColor = defautEntry;
            CityFrameBackgroundColor = defautEntry;
            PasswordFrameBackgroundColor = defautEntry;
            UsernameTextColor = defaultText;
            FirstNameTextColor = defaultText;
            MiddleNameTextColor = defaultText;
            LastNameTextColor = defaultText;
            PhoneNumberTextColor = defaultText;
            CityTextColor = defaultText;
            PasswordTextColor = defaultText;
        }

        #endregion

        #region Methods

        private async void OnRegisterClicked(object obj) 
        {
            if (!(CheckIfEntriesAreEmpty()))
            {
                registerUser = new RegisterUser(Username, Email, FirstName, MiddleName, LastName, City, PhoneNumber, Password);
                HttpResponseMessage response = await dataProvider.RegisterUser(registerUser, TeacherAccount);
                if (response.IsSuccessStatusCode)
                {
                    Toast.MakeText(Android.App.Application.Context, "Account has been created", ToastLength.Short).Show();
                    await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context, "Something went wrong", ToastLength.Short).Show();
                }
            }
        }

        public bool CheckIfEntriesAreEmpty()
        {
            bool isEmpty = false;
            if (String.IsNullOrEmpty(Username))
            {
                Username = $"{nameof(Username)}" + emptyEntryText; 
                UsernameFrameBackgroundColor = emptyEntry;
                UsernameTextColor = emptyEntry;
                isEmpty = true;
            }
            
            if (String.IsNullOrEmpty(Email))
            {
                Email = $"{nameof(Email)}" + emptyEntryText;
                EmailFrameBackgroundColor = emptyEntry;
                EmailTextColor = emptyEntry;
                isEmpty = true;
            }
            
            if (String.IsNullOrEmpty(FirstName)) 
            {
                FirstName = $"{nameof(FirstName)}" + emptyEntryText;
                FirstNameFrameBackgroundColor = emptyEntry;
                FirstNameTextColor = emptyEntry;
                isEmpty = true;
            }
            
            if (String.IsNullOrEmpty(MiddleName))
            {
                MiddleName = $"{nameof(MiddleName)}" + emptyEntryText;
                MiddleNameFrameBackgroundColor = emptyEntry;
                MiddleNameTextColor = emptyEntry;
                isEmpty = true;
            }
            
            if (String.IsNullOrEmpty(LastName))
            {
                LastName = $"{nameof(LastName)}" + emptyEntryText;
                LastNameFrameBackgroundColor = emptyEntry;
                LastNameTextColor = emptyEntry;
                isEmpty = true;
            }
            
            if (String.IsNullOrEmpty(City))
            {
                City = $"{nameof(City)}" + emptyEntryText;
                CityFrameBackgroundColor = emptyEntry;
                CityTextColor = emptyEntry;
                isEmpty = true;
            }
            
            if (String.IsNullOrEmpty(Password))
            {
                Password = $"{nameof(Password)}" + emptyEntryText;
                PasswordFrameBackgroundColor = emptyEntry;
                PasswordTextColor = emptyEntry;
                isEmpty = true;
            }

            if (String.IsNullOrEmpty(PhoneNumber))
            {
                PhoneNumber = $"{nameof(PhoneNumber)}" + emptyEntryText;
                PhoneNumberFrameBackgroundColor = emptyEntry;
                PhoneNumberTextColor = emptyEntry;
                isEmpty = true;
            }

            return isEmpty;
        }

        #endregion
    }
}

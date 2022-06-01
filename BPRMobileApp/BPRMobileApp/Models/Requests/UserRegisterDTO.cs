using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models.Requests
{
    public class UserRegisterDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public UserRegisterDTO(string username, string email, string firstName, string middleName, string lastName, string city, string phone, string password)
        {
            Username = username;
            Email = email;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            City = city;
            PhoneNumber = phone;
            Password = password;

        }
    }
}

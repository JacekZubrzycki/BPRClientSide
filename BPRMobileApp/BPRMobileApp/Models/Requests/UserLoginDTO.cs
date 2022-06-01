using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models.Requests
{
    public class UserLoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserLoginDTO(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}

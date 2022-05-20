using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models
{
    public  class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}

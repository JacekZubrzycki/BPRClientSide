using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models
{
    public  class BookOffer
    {
        public string Listed_Subject_Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public BookOffer(string listed_Subject_Id, string from, string to)
        {
            Listed_Subject_Id = listed_Subject_Id;
            From = from;
            To = to;
        }
    }
}

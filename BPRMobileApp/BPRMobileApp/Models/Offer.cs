using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models
{
    public class Offer
    {

        public string Listed_Subject_Id { get; set; }
        public DataBaseSubject Subject { get; set; }

        public Teacher Teacher { get; set; } 

        public string Price { get; set; }

        public string MinTime { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public Offer (string listed_Subject_Id, DataBaseSubject subject, Teacher teacher,
            string price, string minTime, string from, string to)
        {
            Listed_Subject_Id = listed_Subject_Id;
            Subject = subject;
            Teacher = teacher;
            Price = price;
            MinTime = minTime;
            From = from;
            To = to;
        }

    }
}

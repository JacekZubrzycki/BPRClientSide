using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models
{
    public class CreateOffer
    {
        public int Subject_Id { get; set; }
        public string Price { get; set; }
        public string MinTime { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public CreateOffer(int subject_Id, string price, string minTime, string from, string to)
        {
            Subject_Id = subject_Id;
            Price = price;
            MinTime = minTime;
            From = from;
            To = to;
        }
    }
}

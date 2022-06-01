using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models.Requests
{
    public class OfferDTORequest
    {
        public int Subject_Id { get; set; }
        public decimal Price { get; set; }
        public int MinTime { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public OfferDTORequest(int subject_Id, decimal price, int minTime, DateTime from, DateTime to)
        {
            Subject_Id = subject_Id;
            Price = price;
            MinTime = minTime;
            From = from;
            To = to;
        }
    }
}

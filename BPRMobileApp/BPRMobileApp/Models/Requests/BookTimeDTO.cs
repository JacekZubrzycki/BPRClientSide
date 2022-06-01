using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models.Requests
{
    public class BookTimeDTO
    {
        public int Offer_Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public BookTimeDTO(int offer_Id, DateTime from, DateTime to)
        {
            Offer_Id = offer_Id;
            From = from;
            To = to;
        }
    }
}

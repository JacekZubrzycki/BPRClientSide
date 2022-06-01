using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models.Responses
{
    public class OfferDTOResponse
    {

        public int Offer_Id { get; set; }
        public SubjectDTO Subjects { get; set; }

        public TeacherDTOResponse Teacher { get; set; }

        public decimal Price { get; set; }

        public int MinTime { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public OfferDTOResponse(int offer_Id, SubjectDTO subjects, TeacherDTOResponse teacher,
            decimal price, int minTime, DateTime from, DateTime to)
        {
            Offer_Id = offer_Id;
            Subjects = subjects;
            Teacher = teacher;
            Price = price;
            MinTime = minTime;
            From = from;
            To = to;
        }

    }
}

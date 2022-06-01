using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models.Responses
{
    public class TeacherDTOResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TeacherLocation { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public decimal AvgRating { get; set; }

        public TeacherDTOResponse(string id, string name, string teacherLocation, string phoneNumber, string email, decimal avgRating)
        {
            Id = id;
            Name = name;
            TeacherLocation = teacherLocation;
            PhoneNumber = phoneNumber;
            Email = email;
            AvgRating = avgRating;
        }
    }
}

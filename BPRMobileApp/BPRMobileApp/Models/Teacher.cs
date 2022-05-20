using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models
{
    public class Teacher
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TeacherLocation { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string AvgRating { get; set; }

        public Teacher(string id, string name, string teacherLocation, string phoneNumber, string email, string avgRating)
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

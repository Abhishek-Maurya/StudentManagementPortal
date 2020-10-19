using System;
using System.Collections.Generic;

namespace StudentManagementStudentService.Models
{
    public partial class BasicDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public int CourseDuration { get; set; }
        public string FeeStatus { get; set; }
    }
}

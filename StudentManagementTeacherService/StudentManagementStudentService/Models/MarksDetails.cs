using System;
using System.Collections.Generic;

namespace StudentManagementStudentService.Models
{
    public partial class MarksDetails
    {
        public int Id { get; set; }
        public int? CurrentYear { get; set; }
        public decimal? Sem1 { get; set; }
        public decimal? Sem2 { get; set; }
        public decimal? Sem3 { get; set; }
        public decimal? Sem4 { get; set; }
        public decimal? Sem5 { get; set; }
        public decimal? Sem6 { get; set; }
        public decimal? Sem7 { get; set; }
        public decimal? Sem8 { get; set; }
        public decimal? TotalMaxMarks { get; set; }
        public decimal? TotalObtainedMarks { get; set; }
        public decimal? Percentage { get; set; }
    }
}

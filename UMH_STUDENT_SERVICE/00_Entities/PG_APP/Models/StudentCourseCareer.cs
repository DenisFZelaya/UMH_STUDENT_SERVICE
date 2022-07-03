using System;
using System.Collections.Generic;

namespace UMH_STUDENT_SERVICE.Models
{
    public partial class StudentCourseCareer
    {
        public int? IdCareer { get; set; }
        public int? IdCourse { get; set; }
        public int? Percentaje { get; set; }
        public int? IdPeriod { get; set; }
        public string? Comments { get; set; }
        public string Join { get; set; } = null!;
        public int? Stars { get; set; }
        public string AccountStudent { get; set; } = null!;
        public string? AssignedDate { get; set; }
    }
}

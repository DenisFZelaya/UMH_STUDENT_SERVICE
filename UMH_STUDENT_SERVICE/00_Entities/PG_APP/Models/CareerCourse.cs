using System;
using System.Collections.Generic;

namespace UMH_STUDENT_SERVICE.Models
{
    public partial class CareerCourse
    {
        public int? IdCareer { get; set; }
        public int? IdCourse { get; set; }
        public int? Percentaje { get; set; }
        public bool? HaveRequeriment { get; set; }
        public string? SuggestedPeriod { get; set; }
        public int? OrderNumber { get; set; }
        public string Join { get; set; } = null!;
        public string? AssignedDate { get; set; }
    }
}

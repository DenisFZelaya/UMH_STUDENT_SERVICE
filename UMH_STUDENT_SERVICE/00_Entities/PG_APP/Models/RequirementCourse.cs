using System;
using System.Collections.Generic;

namespace UMH_STUDENT_SERVICE.Models
{
    public partial class RequirementCourse
    {
        public int IdCareer { get; set; }
        public int IdCourse { get; set; }
        public int IdCourseRequirement { get; set; }
        public string Join { get; set; } = null!;
    }
}

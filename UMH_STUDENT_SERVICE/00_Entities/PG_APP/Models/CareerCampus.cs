using System;
using System.Collections.Generic;

namespace UMH_STUDENT_SERVICE.Models
{
    public partial class CareerCampus
    {
        public int IdCampus { get; set; }
        public int IdCareer { get; set; }
        public string Join { get; set; } = null!;
    }
}

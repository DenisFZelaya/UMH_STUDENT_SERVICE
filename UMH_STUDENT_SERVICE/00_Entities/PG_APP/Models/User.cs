using System;
using System.Collections.Generic;

namespace UMH_STUDENT_SERVICE.Models
{
    public partial class User
    {
        public string NumberAcount { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public bool? VerifiedEmail { get; set; }
        public int IdUserType { get; set; }
        public int IdCampus { get; set; }
        public int IdCareer { get; set; }
        public string? RegistrerDate { get; set; }
        public string? StartDate { get; set; }
        public int? IdUserStatus { get; set; }
    }
}

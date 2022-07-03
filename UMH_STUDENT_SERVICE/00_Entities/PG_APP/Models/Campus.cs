using System;
using System.Collections.Generic;

namespace UMH_STUDENT_SERVICE.Models
{
    public partial class Campus
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? NameManager { get; set; }
        public string? MailManager { get; set; }
        public string? Long { get; set; }
        public string? Lat { get; set; }
        public string? CreatedDate { get; set; }
    }
}

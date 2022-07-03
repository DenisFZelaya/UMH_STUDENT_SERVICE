using System;
using System.Collections.Generic;

namespace UMH_STUDENT_SERVICE.Models
{
    public partial class ComplementLearning
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public string? Notes { get; set; }
        public int? StudentLike { get; set; }
    }
}

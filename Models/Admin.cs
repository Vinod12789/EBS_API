using System;
using System.Collections.Generic;

namespace EBS_API.Models
{
    public partial class Admin
    {
        public decimal AdminId { get; set; }
        public string AdminUsername { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
    }
}

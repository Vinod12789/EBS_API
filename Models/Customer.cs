using System;
using System.Collections.Generic;

namespace EBS_API.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
            Payments = new HashSet<Payment>();
        }

        public decimal CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public decimal CustomerMobile { get; set; }
        public string CustomerAddress { get; set; } = null!;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

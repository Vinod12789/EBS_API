using System;
using System.Collections.Generic;

namespace EBS_API.Models
{
    public partial class Bill
    {
        public Bill()
        {
            Payments = new HashSet<Payment>();
        }

        public decimal BillId { get; set; }
        public DateTime BillGenDate { get; set; }
        public decimal CustomerId { get; set; }
        public double PerUnitCost { get; set; }
        public double TotalUnits { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDueDate { get; set; }
        public string? CustomerName { get; set; }
        public decimal? CustomerMobile { get; set; }
        public string? CustomerAddress { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

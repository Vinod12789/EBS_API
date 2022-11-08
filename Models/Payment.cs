using System;
using System.Collections.Generic;

namespace EBS_API.Models
{
    public partial class Payment
    {
        public decimal PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal CustomerId { get; set; }
        public decimal BillId { get; set; }
        public string PaymentStatus { get; set; } = null!;

        public virtual Bill Bill { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}

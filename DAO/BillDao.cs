namespace EBS_API.DAO
{
    public class BillDao
    {

            public decimal BillId { get; set; }
            public DateTime BillGenDate { get; set; }
            public decimal CustomerId { get; set; }
            public double PerUnitCost { get; set; }
            public double TotalUnits { get; set; }
            public decimal BillAmount { get; set; }
            public DateTime BillDueDate { get; set; }
    }
}

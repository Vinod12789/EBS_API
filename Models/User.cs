namespace EBS_API.Models
{
    public class User
    {
        public decimal CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
    
        public string CustomerAddress { get; set; } = null!;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public decimal CustomerMobile { get; set; }

    }
}

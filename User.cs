namespace EBS_API
{
    public class User
    {
        public string UserName { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public decimal CustomerMobile { get; set; }
        public string CustomerPassword { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

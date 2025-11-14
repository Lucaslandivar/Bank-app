namespace _7Bank.Api.DTOs
{
    public class AccountUpdateDto
    {
        public int AccountId { get; set; }
        public string? AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsActive { get; set; }
    }

}

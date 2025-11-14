namespace _7Bank.Api.DTOs.Transaction
{
    public class TransactionResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public decimal? NewBalance { get; set; }
    }
}


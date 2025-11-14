namespace _7Bank.Api.DTOs
{
    public class PixRequestDto
    {
        public int FromUserId { get; set; }
        public string Identifier { get; set; } = string.Empty;
        public string IdentifierType { get; set; } = "email"; // opcional: "email" ou "cpf"
        public decimal Amount { get; set; }
    }
}

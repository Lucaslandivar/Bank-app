using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace _7Bank.Api.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(10)]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public bool IsActive { get; set; }


        [ForeignKey("UserId")]
        [JsonIgnore]
        public Users? Users { get; set; }
        public int UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

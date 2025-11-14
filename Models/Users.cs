using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text.Json.Serialization;


namespace _7Bank.Api.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty ;

        [Required]
        [MaxLength(100)]
        public required string Password { get; set; }

        [Required]
        [MaxLength(11)]
        [MinLength(11)]
        public required string Cpf {  get; set; }

        [Required]
        [MaxLength(20)]
        public required string Role { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public Account? Account { get; set; }
    }
}

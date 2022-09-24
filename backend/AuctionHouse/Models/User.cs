using System.ComponentModel.DataAnnotations;

namespace AuctionHouse.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Must be between 6 and 100 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool EmailVerified { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

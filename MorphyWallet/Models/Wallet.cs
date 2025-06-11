using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MorphyWallet.Models
{
    public class Wallet
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "PlanName is required")]
        public string PlanName { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        [Range(0, 100000)]
        public decimal Balance { get; set; }

        public decimal Limit { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}

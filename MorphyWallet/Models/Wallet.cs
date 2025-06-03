using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MorphyWallet.Models
{
    public class Wallet
        {
            public int Id { get; set; }

            [Required]
            public string UserId { get; set; }

            [DataType(DataType.Currency)]
            public decimal Balance { get; set; } = 0;

            [ForeignKey("UserId")]
            public ApplicationUser User { get; set; }
        }
    }

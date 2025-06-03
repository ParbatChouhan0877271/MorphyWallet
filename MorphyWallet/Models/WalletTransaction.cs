using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MorphyWallet.Models
{
    public enum TransactionType { Credit, Debit }

    public class WalletTransaction
    {
        public int Id { get; set; }

        [Required]
        public int WalletId { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }
    }
}

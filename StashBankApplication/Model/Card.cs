using StashBankApplication.Domain.Enums;
using StashBankApplication.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StashBankApplication.Model
{
    [Table("card")]
    public class Card : BaseEntity
    {
        [Column("account_id")]
        public long AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }

        [Required]
        [Column("tier")]
        public CardTier Tier { get; set; }

        [Required]
        [Column("credit_limit")]
        public decimal CreditLimit { get; set; }

        [Required]
        [Column("available_credit")]
        public decimal AvailableCredit { get; set; }

        [Column("last_upgrade_at")]
        public DateTime? LastUpgradeAt { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}

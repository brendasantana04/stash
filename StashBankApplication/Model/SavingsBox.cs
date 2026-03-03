using StashBankApplication.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace StashBankApplication.Model
{
    [Table("SavingsBox")]
    public class SavingsBox : BaseEntity
    {
        public long AccountId { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}

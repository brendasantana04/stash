using Microsoft.VisualBasic;
using StashBankApplication.Domain.Enums;
using StashBankApplication.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StashBankApplication.Model
{
    [Table("transaction")]
    public class Transaction : BaseEntity
    {
        public long accountid { get; set; }
        public TransactionType type { get; set; }
        public decimal value { get; set; }
        public Account Account { get; set; }
        public string description { get; set; }
    }
}

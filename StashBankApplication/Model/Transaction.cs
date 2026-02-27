using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StashBankApplication.Model
{
    [Table("transaction")]
    public class Transaction
    {
        [Key]
        [Column("id")]
        public long id { get; set; }
        public long accountid { get; set; }
        public enum type;
        public decimal value { get; set; }
        public string description { get; set; }
        public DateTime createdon { get; set; }

    }
}

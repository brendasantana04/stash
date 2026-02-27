using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StashBankApplication.Model
{
    [Table("transfer")]
    public class Transfer
    {
        [Key]
        [Column("id")]
        public long id { get; set; }
        public long id_account_to { get; set; }
        public long id_account_from { get; set; }
        public decimal value { get; set; }
        public DateTime createdon { get; set; }
    }
}

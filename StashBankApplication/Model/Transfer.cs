using StashBankApplication.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StashBankApplication.Model
{
    [Table("transfer")]
    public class Transfer : BaseEntity
    {
        public long id_account_to { get; set; }
        public long id_account_from { get; set; }
        public decimal value { get; set; }
    }
}

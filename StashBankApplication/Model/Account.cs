using StashBankApplication.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StashBankApplication.Model
{
    [Table("account")]
    public class Account : BaseEntity
    {
        [Required]
        [Column("user_id")]
        [ForeignKey("user")]
        public long userid { get; set; }
        public User user { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(40)")]
        public string numeroconta { get; set; }
        
        [Required]
        [Column("funds", TypeName = "decimal(18, 2)")]
        public decimal funds { get; set; }
        
        [Required]
        [Column("active", TypeName = "bool")]
        public bool active { get; set; }
    }
}

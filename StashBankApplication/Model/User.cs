using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StashBankApplication.Model.Base;

namespace StashBankApplication.Model
{
    [Table("user")]
    public class User : BaseEntity
    {
        [Required]
        [Column("name", TypeName = "varchar(40)")]
        [MaxLength(40)]
        public string name { get; set; }
        
        [Required]
        [Column("email", TypeName = "varchar(30)")]
        [MaxLength(30)]
        public string email { get; set; }
        
        [Required]
        [Column("password", TypeName = "varchar(30)")]
        [MaxLength(30)]
        public string password { get; set; }
    }
}

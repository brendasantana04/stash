using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StashBankApplication.Model.Base;

namespace StashBankApplication.Model
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Required]
        [Column("username", TypeName = "varchar(20)")]
        [MaxLength(20)]
        public string username { get; set; }

        [Required]
        [Column("firstName", TypeName = "varchar(40)")]
        [MaxLength(40)]
        public string firstName { get; set; }

        [Required]
        [Column("lastName", TypeName = "varchar(40)")]
        [MaxLength(40)]
        public string lastName { get; set; }

        [Required]
        [Column("email", TypeName = "varchar(30)")]
        [MaxLength(40)]
        public string email { get; set; }

        [Required]
        [Column("phoneNumber", TypeName = "varchar(11)")]
        [MaxLength(11)]
        public string phoneNumber { get; set; }
        
        [Required]
        [Column("password", TypeName = "varchar(30)")]
        [MaxLength(30)]
        public string password { get; set; }

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}

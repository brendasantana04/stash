using System.ComponentModel.DataAnnotations;

namespace StashBankApplication.DTOs.Files
{
    public class FileUpload
    {
        [Required]
        public IFormFile File { get; set; }

    }
}

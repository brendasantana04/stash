using System.ComponentModel.DataAnnotations;

namespace StashBankApplication.DTOs.Files
{
    public class MultipleFilesUploadDTO
    {
        [Required]
        public List<IFormFile> Files { get; set; }
    }
}

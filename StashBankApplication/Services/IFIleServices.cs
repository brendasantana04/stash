using StashBankApplication.DTOs.Files;

namespace StashBankApplication.Services
{
    public interface IFIleServices
    {
        byte[] GetFile(string fileName);
        Task<FileDetailDTO> SaveFileToDisk(IFormFile file);
        Task<List<FileDetailDTO>> SaveFilesToDisk(List<IFormFile> files);


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StashBankApplication.DTOs.Files;
using StashBankApplication.Services;

namespace StashBankApplication.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class FileController : ControllerBase
    {
        private IFIleServices _fileServices;
     
        public FileController(IFIleServices fileServices)
        {
            _fileServices = fileServices;
        }

        [HttpGet("downloadFile/{fileName}")]
        [Produces("application/octet-stream")]
        public IActionResult DownloadFile(string fileName)
        {
            var buffer = _fileServices.GetFile(fileName);
            if (buffer == null || buffer.Length == 0)
                return NoContent();

            var contentType = $"application/{Path.GetExtension(fileName).TrimStart('.')}";
            return File(buffer, contentType, fileName);
        }

        [HttpPost("uploadFile")]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest("Arquivo não enviado");

            var fileDetail = await _fileServices.SaveFileToDisk(file);
            return Ok(fileDetail);
        }

        [HttpPost("uploadFiles")]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> UploadFiles(
            [FromForm] MultipleFilesUploadDTO input)
        {
            if (input.Files == null || !input.Files.Any())
                return BadRequest("Nenhum arquivo enviado");

            var details = await _fileServices.SaveFilesToDisk(input.Files);
            return Ok(details);
        }
    }
}

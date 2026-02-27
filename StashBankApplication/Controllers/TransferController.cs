using Microsoft.AspNetCore.Mvc;
using StashBankApplication.Model;
using StashBankApplication.Services;

namespace StashBankApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        ITransferServices _transferService;

        public TransferController(ITransferServices transferService)
        {
            _transferService = transferService;
        }

        [HttpPost]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            await _transferService.TransferAsync(request);
            return Ok("Transferência realizada com sucesso.");
        }
    }
}

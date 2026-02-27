using Microsoft.AspNetCore.Mvc;
using StashBankApplication.DTOs.Statement;
using StashBankApplication.Services;

namespace StashBankApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly ITransferServices _transferServices;

        public StatementController(ITransferServices transferServices)
        {
            _transferServices = transferServices;
        }

        [HttpPost]
        public async Task<IActionResult> GetStatement([FromBody] StatementRequest request)
        {
            var result = await _transferServices.GetStatementAsync(request);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StashBankApplication.DTOs.Deposit;
using StashBankApplication.Model;
using StashBankApplication.Services;

namespace StashBankApplication.Controllers
{
    [ApiController]
    [Route("api/box")]
    public class SavingsBoxController : ControllerBase
    {
        private ISavingsBoxServices _service;

        public SavingsBoxController(ISavingsBoxServices service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.FindAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] SavingsBox savingsBox)
        {
            var createdBox = _service.Create(savingsBox);
            if (createdBox == null) return NotFound();
            return Ok(createdBox);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(DepositBoxRequest request)
        {
            var result = await _service.DepositToBox(
                request.AccountId,
                request.BoxId,
                request.Amount);

            if (!result)
                return BadRequest("Erro ao depositar.");

            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(DepositBoxRequest request)
        {
            var result = await _service.WithdrawFromBox(
                request.AccountId,
                request.BoxId,
                request.Amount);

            if (!result)
                return BadRequest("Erro ao sacar.");

            return Ok();
        }
    }
}

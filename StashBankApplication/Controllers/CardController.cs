using Microsoft.AspNetCore.Mvc;
using StashBankApplication.DTOs.Transfer;
using StashBankApplication.Model;
using StashBankApplication.Services;

namespace StashBankApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_cardService.FindAll());
        }

        [HttpPost("{accountId}/credit-transaction")]
        public async Task<IActionResult> MakeCreditTransaction(long accountId, [FromBody] TransferRequest request)
        {
            await _cardService.MakeCreditTransaction(accountId, request.value);
            return NoContent();
        }

        [HttpPost("{accountId}/pay-bill")]
        public async Task<IActionResult> PayBill(long accountId, [FromBody] TransferRequest request)
        {
            await _cardService.PayCreditBill(accountId, request.value);
            return NoContent();
        }

        //POST /api/card/1/upgrade/
        [HttpPost("{accountId}/upgrade")]
        public async Task<IActionResult> UpgradeCard(long accountId)
        {
            await _cardService.UpgradeCard(accountId);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StashBankApplication.Model;
using StashBankApplication.Services;

namespace StashBankApplication.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_accountServices.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var account = _accountServices.FindById(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account account)
        {
            var createdAccount = _accountServices.Create(account);
            if (createdAccount == null) return NotFound();
            return Ok(createdAccount);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Account account)
        {
            var createdAccount = _accountServices.Update(account);
            if (createdAccount == null) return NotFound();
            return Ok(createdAccount);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] Account account)
        {
            _accountServices.Delete(account);
            return NoContent();
        }
    }
}

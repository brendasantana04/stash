using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StashBankApplication.Model;
using StashBankApplication.Services;
using System.Security.Cryptography.Pkcs;

namespace StashBankApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userServices.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var user = _userServices.FindById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var createdUser = _userServices.Create(user);
            if (createdUser == null) return NotFound();
            return Ok(createdUser);
        }

        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            var createdUser = _userServices.Update(user);
            if (createdUser == null) return NotFound();
            return Ok(createdUser);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] User user)
        {
            _userServices.Delete(user);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StashBankApplication.Model.Context;
using StashBankApplication.Services;

namespace StashBankApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        public readonly MSSQLContext _context;

        public TransactionController(MSSQLContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Transactions.ToList());
        }
    }
}

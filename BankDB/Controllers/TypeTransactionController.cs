using BankDB.Models;
using BankDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeTransactionController : ControllerBase
    {
        private readonly ITypeTransactionService _typeTransactionService;

        public TypeTransactionController(ITypeTransactionService typeTransactionService)
        {
            _typeTransactionService = typeTransactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeTransaction>>> GetTypeTransactions()
        {
            var typeTransactions = await _typeTransactionService.GetAllTypeTransactionsAsync();
            return Ok(typeTransactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeTransaction>> GetTypeTransaction(int id)
        {
            var typeTransaction = await _typeTransactionService.GetTypeTransactionByIdAsync(id);
            if (typeTransaction == null)
            {
                return NotFound();
            }
            return Ok(typeTransaction);
        }

        [HttpPost]
        public async Task<ActionResult<TypeTransaction>> CreateTypeTransaction([FromBody] TypeTransaction typeTransaction)
        {
            var createdTypeTransaction = await _typeTransactionService.AddTypeTransactionAsync(typeTransaction);
            return CreatedAtAction(nameof(GetTypeTransaction), new { id = createdTypeTransaction.TypeTransactionId }, createdTypeTransaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTypeTransaction(int id, [FromBody] TypeTransaction typeTransaction)
        {
            if (id != typeTransaction.TypeTransactionId)
            {
                return BadRequest();
            }
            await _typeTransactionService.UpdateTypeTransactionAsync(typeTransaction);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeTransaction(int id)
        {
            await _typeTransactionService.DeleteTypeTransactionAsync(id);
            return NoContent();
        }
    }
}

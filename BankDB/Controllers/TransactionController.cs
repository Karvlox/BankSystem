using BankDB.Models;
using BankDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankDB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] Transaction transaction)
    {
        var createdTransaction = await _transactionService.AddTransactionAsync(transaction);
        return CreatedAtAction(nameof(GetTransaction), new { id = createdTransaction.TransactionId }, createdTransaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction transaction)
    {
        if (id != transaction.TransactionId)
        {
            return BadRequest();
        }
        await _transactionService.UpdateTransactionAsync(transaction);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        await _transactionService.DeleteTransactionAsync(id);
        return NoContent();
    }

    [HttpGet("sender/{senderId}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsBySenderId(int senderId)
    {
        var transactions = await _transactionService.GetTransactionsBySenderIdAsync(senderId);
        return Ok(transactions);
    }

    [HttpGet("receiver/{receiverId}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByReceiverId(int receiverId)
    {
        var transactions = await _transactionService.GetTransactionsByReceiverIdAsync(receiverId);
        return Ok(transactions);
    }

}
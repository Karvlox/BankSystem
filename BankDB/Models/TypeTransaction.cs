namespace BankDB.Models;

public class TypeTransaction
{
    public int TypeTransactionId { get; set; }
    public string Name { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }
}
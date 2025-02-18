namespace BankDB.Models;

public class Transaction
{
    public int TransactionId { get; set; }
    public int SenderId { get; set; }
    public int RecivedId { get; set; }
    public int TypeTransactionId { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public Account? Sender { get; set; }
    public Account? Receiver { get; set; }
    public TypeTransaction? TypeTransaction { get; set; }
}
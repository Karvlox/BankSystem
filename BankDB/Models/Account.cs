namespace BankDB.Models;

public class Account
{
    public int AccountID { get; set; }
    public int? UserID { get; set; }
    public double Balance { get; set; }

    public User? User { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
}
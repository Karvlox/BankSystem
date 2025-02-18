namespace BankDB.Models;

public class User
{
    public int Id { get; set; }
    public int CI { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int NumberPhone { get; set; }
    public string Email { get; set; }
    public string? LicensePath { get; set; }
    public string? FaceBase64 { get; set; }

    public ICollection<Account>? Accounts { get; set; }
    public ICollection<UserAccess>? UserAccesses { get; set; }
    public ICollection<Transaction>? SentTransactions { get; set; }
    public ICollection<Transaction>? ReceivedTransactions { get; set; }
}
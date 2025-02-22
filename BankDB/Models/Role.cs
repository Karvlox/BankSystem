namespace BankDB.Models;

public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public int AccessLevel { get; set; }

    public ICollection<UserAccess>? UserAccesses { get; set; }
}
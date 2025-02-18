using System.Text.Json.Serialization;

namespace BankDB.Models;

public class UserAccess
{
    public int UserAccessId { get; set; }
    public int UserId { get; set; }
    public int NumberAccess { get; set; }
    public string Password { get; set; }
    public int? RoleId { get; set; }

    [JsonIgnore] 
    public User? User { get; set; }
    public Role? Role { get; set; }
}
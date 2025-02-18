namespace AtmService.Models;

public class Atm
{

    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Location { get; set; }
    public int AccountId { get; set; }
}
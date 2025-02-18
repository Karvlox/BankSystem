namespace AtmService.Models;

public class AtmStatus
{
    public Guid Id { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime Date { get; set; }
}

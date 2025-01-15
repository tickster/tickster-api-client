namespace TicksterSampleApp.Domain.Models;

public class ImportLog
{
    public int TicksterCrmId { get; set; }
    public DateTime Date { get; set; } = new();
}

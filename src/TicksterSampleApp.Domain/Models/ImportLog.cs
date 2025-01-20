namespace TicksterSampleApp.Domain.Models;

public class ImportLog
{
    public int LastTicksterCrmId { get; set; }
    public DateTime Date { get; set; } = new();
}

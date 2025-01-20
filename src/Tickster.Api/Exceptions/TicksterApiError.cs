namespace Tickster.Api.Exceptions;
public class TicksterApiError : Exception
{

    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
    public int Status { get; set; }
    public string[] AdditionalProperties { get; set; } = [];

    public TicksterApiError(string message) : base(message)
    {
    }

    public TicksterApiError(string message, Exception innerException) : base(message, innerException)
    {
    }
}

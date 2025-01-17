using System.Security.Cryptography.X509Certificates;
using Tickster.Api.Dtos;

namespace Tickster.Api.Exceptions;
public class TicksterResponseError : Exception
{

    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
    public int Status { get; set; }
    public string[] AdditionalProperties { get; set; } = [];

    public TicksterResponseError(string message) : base(message)
    {
    }

    public TicksterResponseError(string message, Exception innerException) : base(message, innerException)
    {
    }
}

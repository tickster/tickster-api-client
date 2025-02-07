namespace TicksterSampleApp.Infrastructure.Configuration;

public class TicksterCrmConfig
{
    public required string Endpoint { get; set; } = string.Empty;
    public required string ApiKey { get; set; } = string.Empty;
    public required string EogRequestCode { get; set; } = string.Empty;
    public required string Login { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}

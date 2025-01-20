namespace Tickster.Api;

public class TicksterOptions
{
    public string Endpoint { get; set; } = "https://api.tickster.com";
    public string ApiKey { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string EogRequestCode { get; set; } = string.Empty;
    public int DefaultResultLimit { get; set; } = 500;
    public string DefaultLanguage { get; set; } = "sv";
}
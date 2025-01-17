namespace Tickster.Api.Dtos;
internal class ErrorResponse
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
    public int Status { get; set; }
    public string AdditionalProp1 { get; set; } = string.Empty;
    public string AdditionalProp2 { get; set; } = string.Empty;
    public string AdditionalProp3 { get; set; } = string.Empty;
}

namespace TicksterSampleApp.Domain.Models;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TicksterUserRefNo { get; set; } = string.Empty;
    public string? IdNumber { get; set; } = string.Empty;
    public bool IsCompany { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PostalAddressLineOne { get; set; } = string.Empty;
    public string? PostalAddressLineTwo { get; set; } = string.Empty;
    public string? ZipCode { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? CountryCode { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;
    public string? MobilePhone { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
}

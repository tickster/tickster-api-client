﻿namespace Tickster.Api.Models.Event;

public class Price
{
    public double Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
}
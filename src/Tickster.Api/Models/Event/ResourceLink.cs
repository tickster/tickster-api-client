﻿namespace Tickster.Api.Models.Event;

public class ResourceLink
{
    public string Rel { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
}
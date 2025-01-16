# Tickster API Client

This is the Tickster API client library. It provides both low-level and high-level abstractions for interacting with the Tickster API. The client can be used either with dependency injection (DI) or without it, providing flexibility based on your application's architecture.

## Installation

Add the `Tickster.Api` package to your project:

```bash
> dotnet add package Tickster.Api
```

---

## Usage

### Option 1: Using Dependency Injection (Recommended)

1. In your `Startup` class or `Program.cs`, add the `TicksterClient` to the DI container:

```csharp
using Tickster.Api.Extensions;

var services = new ServiceCollection();
services.AddTicksterApi(options =>
{
    options.Endpoint = "https://api.tickster.com";
    options.ApiKey = "your-api-key";
});
```

2. Inject the `TicksterClient` wherever needed:

```csharp
using Tickster.Api;

public class MyService
{
    private readonly TicksterClient _client;

    public MyService(TicksterClient client)
    {
        _client = client;
    }

    public async Task DoSomethingAsync()
    {
        var response = await _client.Agent.HttpClient.GetAsync("/some-endpoint");
        // Handle response
    }
}
```

### Option 2: Without Dependency Injection

If your application does not use DI, you can instantiate the client manually using the `TicksterApiFactory`:

```csharp
using Tickster.Api;

var options = new TicksterApiOptions
{
    Endpoint = "https://api.tickster.com",
    ApiKey = "your-api-key"
};

var factory = new TicksterApiFactory(options);
var httpClient = new HttpClient();
var client = factory.Create(httpClient);

// Use the client
var response = await client.Agent.HttpClient.GetAsync("/some-endpoint");
```

---

## Configuration Options
The `TicksterApiOptions` class provides the following configuration properties:

| Property | Description |
|----------|-------------|
| `Endpoint` | The base URL for the Tickster API (default: `https://api.tickster.com`). |
| `ApiKey`   | Your API key for authenticating with the Tickster API. |

---

## License

This project is licensed under the MIT License.


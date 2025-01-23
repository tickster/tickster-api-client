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
        var purchases = await _client.GetCrmPurchases(1234);
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
var purchases = await client.GetCrmPurchases(1234);
```

## Low-level API

The low-level API provides direct access to the TicksterHttpAgent class used to build HTTP requests and handle API exceptions, as well as the underlying HttpClient used to perform the requests.

 * `TicksterClient.Agent` - Builds and executes requests, handles errors
 * `TicksterClient.Agent.HttpClient` - Underlying, pre-configured HttpClient

```csharp

// Retrieve raw, unparsed JSON for a call to the CRM API
var rawResponseContent = await _client.Agent.MakeCrmRequest("", 12345, 100, "sv");

// Retrieve the HttpResponseMessage object for the same call
var response = await _client.Agent.HttpClient.GetAsync($"/api/sv/0.4/crm/{_client.Options.EogRequestCode}/12345/100?key={_client.Options.ApiKey}");


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


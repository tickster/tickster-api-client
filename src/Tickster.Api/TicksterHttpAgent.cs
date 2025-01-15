namespace Tickster.Api;

public class TicksterHttpAgent(HttpClient client)
{
    public HttpClient HttpClient => client;
}
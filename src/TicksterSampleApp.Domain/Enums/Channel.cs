namespace TicksterSampleApp.Domain.Enums;

public enum Channel
{
    Undefined = 0,
    Webshop = 1,
    Backoffice = 2,
    TicksterPhoneSales = 3
}

public static class ChannelExtensions
{
    public static Channel FromString(string channel)
    {
        channel = channel.Trim().ToLower();

        return channel switch
        {
            "webshop" => Channel.Webshop,
            "backoffice" => Channel.Backoffice,
            "ticksterPhoneSales" => Channel.TicksterPhoneSales,
            _ => Channel.Undefined
        };
    }
}
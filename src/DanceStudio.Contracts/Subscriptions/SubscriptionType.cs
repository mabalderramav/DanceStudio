using System.Text.Json.Serialization;

namespace DanceStudio.Contracts.Subscriptions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SubscriptionType
    {
        Free,
        Starter,
        Pro,
    }
}

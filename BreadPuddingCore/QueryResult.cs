namespace BreadPudding;

using Newtonsoft.Json;

internal class QueryResult
{
    [JsonProperty("status")] public string? Status { get; set; }
    [JsonProperty("menu")] public MenuResult? Menu { get; set; }

}
namespace BreadPudding;

using Newtonsoft.Json;

internal class QueryResult
{
    [JsonProperty("status")] public string? Status { get; set; }
    [JsonProperty("menu")] public MenuResult? Menu { get; set; }
    [JsonProperty("periods")] public List<PeriodHeader>? Periods { get; set; }

}

internal class PeriodHeader
{
    [JsonProperty("id")] public string? ID { get; set; }
    [JsonProperty("name")] public string? Name { get; set; }
    public override string ToString() => Name ?? string.Empty;
}
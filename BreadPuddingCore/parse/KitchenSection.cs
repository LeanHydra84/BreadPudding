namespace BreadPudding;

using Newtonsoft.Json;
using System.Collections.Generic;

public class KitchenSection
{
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("items")] public List<Food?>? Food { get; set; }

    public override string ToString() => Name ?? string.Empty;

}
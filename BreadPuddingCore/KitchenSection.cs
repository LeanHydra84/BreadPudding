namespace BreadPudding;

using Newtonsoft.Json;
using System.Collections.Generic;

public class KitchenSection
{
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("items")] public List<Food?>? Food { get; set; }
}
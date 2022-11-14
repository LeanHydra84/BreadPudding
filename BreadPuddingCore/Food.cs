namespace BreadPudding;

using Newtonsoft.Json;

public class Food
{
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("description")] public string? Description { get; set; }
    [JsonProperty("ingredients")] public string? Ingredients { get; set; }
}
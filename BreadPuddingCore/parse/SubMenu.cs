namespace BreadPudding;

using Newtonsoft.Json;
using System.Collections.Generic;

public class SubMenu
{
    [JsonProperty("name")] public string? Period { get; set; }
    [JsonProperty("sort_order")] public int SortOrder { get; set; }
    [JsonProperty("categories")] public List<KitchenSection?>? Items { get; set; }
}
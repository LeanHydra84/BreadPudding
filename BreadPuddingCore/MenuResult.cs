namespace BreadPudding;

using System.Collections.Generic;
using Newtonsoft.Json;

public class MenuResult
{
    [JsonProperty("date")] public string? Date { get; set; }

    [JsonProperty("periods")] public SubMenu? SubMenu { get; set; }
}
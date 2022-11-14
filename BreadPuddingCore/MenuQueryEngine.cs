namespace BreadPudding;

using System;
using System.Net.Http;
using Newtonsoft.Json;

public enum FoodPeriod
{
    Breakfast,
    Lunch,
    Dinner,
}

public class MenuQueryEngine
{
    private HttpClient client;

    // Append date in format "2022-11-9" -- Year-Month-Day

    private const string Breakfast = "636dde12351d530589bfd652";
    private const string Dinner = "636dde12351d530589bfd664";
    private const string Lunch = "636dde12351d530589bfd657";

    public MenuQueryEngine()
    {
        client = new HttpClient();
    }

    public async Task<SubMenu?> QueryPeriod(DateTime date, FoodPeriod period)
    {
        string per = period switch {
            FoodPeriod.Breakfast => Breakfast,
            FoodPeriod.Lunch => Lunch,
            FoodPeriod.Dinner => Dinner,
            _ => "",
        };

        string request_uri = $"https:\\\\api.dineoncampus.com/v1/location/5871478b3191a200db4e6a2b/periods/{per}?platform=0&date={date.Date.ToString("yyyy-MM-dd")}";
        Uri uri = new Uri(request_uri);

        HttpResponseMessage? response = await client.GetAsync(uri);
        if(response == null) return null;

        string read = await response.Content.ReadAsStringAsync();
        QueryResult? result = JsonConvert.DeserializeObject<QueryResult>(read);
        if(result?.Status != "success") return null;
        return result?.Menu?.SubMenu;
    }

    public async Task<SubMenu?[]> Query(DateTime date)
    {
        SubMenu?[] subs = new SubMenu[3];
        subs[0] = await QueryPeriod(date, FoodPeriod.Breakfast);
        subs[1] = await QueryPeriod(date, FoodPeriod.Breakfast);
        subs[2] = await QueryPeriod(date, FoodPeriod.Breakfast);
        return subs;
    }

}
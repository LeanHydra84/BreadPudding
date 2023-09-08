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

    // Example URL: https://api.dineoncampus.com/v1/location/5871478b3191a200db4e6a2b/periods/64e7ebcbc625af0a3894eac9?platform=0&date=2023-8-24
    private const string Breakfast = "636dde12351d530589bfd652";
    private const string Dinner = "636dde12351d530589bfd664";
    private const string Lunch = "636dde12351d530589bfd657";

    public MenuQueryEngine()
    {
        client = new HttpClient();
    }

    private async Task<QueryResult?> QueryBase(DateTime date)
    {
        string dateTimeFormatted = date.Date.ToString("yyyy-M-dd");
        string reqURL = $"https://api.dineoncampus.com/v1/location/5871478b3191a200db4e6a2b/periods?platform=0&date={dateTimeFormatted}";

        Uri uri = new Uri(reqURL);

        var response = await client.GetAsync(uri);
        string responseJson = await response.Content.ReadAsStringAsync();

        QueryResult? queryResult = JsonConvert.DeserializeObject<QueryResult>(responseJson);
        if (queryResult?.Status != "success") return null;
        return queryResult;
    }



    public async Task<SubMenu?> QueryPeriod(DateTime date, string? periodID)
    {
        if (periodID == null) return null;
        string dateAsString = date.Date.ToString("yyyy-M-dd");
        string request_uri = $"https:\\\\api.dineoncampus.com/v1/location/5871478b3191a200db4e6a2b/periods/{periodID}?platform=0&date={dateAsString}";
        
        Uri uri = new Uri(request_uri);

        HttpRequestMessage message = new HttpRequestMessage();
        message.RequestUri = uri;
        
        message.Method = HttpMethod.Get;

        HttpResponseMessage? response = await client.SendAsync(message);
        if(response == null) return null;

        string read = await response.Content.ReadAsStringAsync();

        QueryResult? result = JsonConvert.DeserializeObject<QueryResult>(read);
        if(result?.Status != "success") return null;
        return result?.Menu?.SubMenu;
    }

    private string? GetPeriodID(QueryResult? query, string name) => query?.Periods?.Find(a => a.Name == name)?.ID;

    public async Task<SubMenu[]> Query(DateTime date)
    {
        QueryResult? result = await QueryBase(date);
        // intial querybase comes with a side of breakfast yum
        List<SubMenu?> subs = new();

        subs.Add(result?.Menu?.SubMenu);
        subs.Add(await QueryPeriod(date, GetPeriodID(result, "Lunch")));
        subs.Add(await QueryPeriod(date, GetPeriodID(result, "Dinner")));
        return subs.Where(a => a != null).Cast<SubMenu>().ToArray();
    }

}
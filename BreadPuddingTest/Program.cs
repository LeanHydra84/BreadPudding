using System;
using BreadPudding;

public class TestSuite
{

    static async Task Main()
    {
        MenuQueryEngine engine = new MenuQueryEngine();
        SubMenu? result = await engine.QueryPeriod(DateTime.Now, FoodPeriod.Lunch);

        Console.WriteLine(result?.Period + ":");
        Console.WriteLine("\t" + result?.Items?[0]?.Name);
    }

}
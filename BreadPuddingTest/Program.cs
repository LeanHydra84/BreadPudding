using System;
using System.IO;
using System.Reflection;
using BreadPudding;
using BreadPudding.Caching;

public class TestSuite
{

    static async Task Main()
    {
        Console.WriteLine(Assembly.GetExecutingAssembly().Location);
        BreadCacheManager builder = new BreadCacheManager("data");

        builder.FoodOptions = FoodData.Name;
        MenuParser parser = builder.Parser;

        //parser.AddRule((Food a) => a.Name.Contains("Eggs", StringComparison.CurrentCultureIgnoreCase));
        //parser.AddRule((KitchenSection a) => a.Name.Contains("Sweet"));

        await builder.QuickBuildCacheWeek(DateTime.Now);
        BreadWeek week = await builder.GetWeek(DateTime.Now);
        

        foreach(BreadDay? day in week.Days)
        {
            if (day == null) continue;
            Console.WriteLine("New Day: ");
            foreach (Food foo in day.Data)
                Console.WriteLine(foo);
        }

    }

}
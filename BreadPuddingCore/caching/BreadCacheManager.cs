using BreadPudding.Caching;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadPudding
{
    public class BreadCacheManager
    {

        private MenuQueryEngine queryEngine;
        private BreadCacher cacher;

        private MenuParser preParser;


        public FoodData FoodOptions { get; set; }

        public MenuParser Parser => preParser;
        public BreadCacher Cacher => cacher;

        public string Path => cacher.Path;

        public event Action<DateTime> OnDayUpdated;

        public BreadCacheManager(string path)
        {
            queryEngine = new MenuQueryEngine();
            preParser = new MenuParser();
            cacher = new BreadCacher(path);
            OnDayUpdated = new Action<DateTime>(delegate { });
            FoodOptions = FoodData.Name;
        }

        public async Task<BreadDay> QueryNewBreadDay(DateTime date)
        {
            BreadDay day = new BreadDay(date);

            SubMenu[] menu = await queryEngine.Query(day.Date);
            day.Data = preParser.ParseMenus(menu, FoodOptions);
            return day;
        }

        private void UpdateWeek(DateTime startWeek)
        {
            for (int i = 0; i < 7; i++)
            {
                DateTime day = startWeek.AddDays(i);
                OnDayUpdated?.Invoke(day);
            }
        }

        public async Task<BreadWeek> QuickBuildCacheWeek(DateTime date)
        {
            BreadWeek week = new BreadWeek(date);
            DateTime startWeek = week.Date;

            for(int i = 0; i < 7; i++)
            {
                DateTime dayDate = startWeek.AddDays(i);
                BreadDay day = await QueryNewBreadDay(dayDate);
                week.Days[i] = day;
            }

            cacher.CacheWeek(week);
            UpdateWeek(startWeek);
            return week;
        }

        public async Task<BreadDay> InvalidateDay(DateTime date)
        {
            BreadDay newDay = await QueryNewBreadDay(date);
            cacher.Cache(newDay);
            OnDayUpdated?.Invoke(newDay.Date);
            return newDay;
        }

        public async Task<BreadDay> GetDay(DateTime date)
        {
            BreadDay? day = cacher.GetDay(date);
            if(day == null)
            {
                await QuickBuildCacheWeek(date);
                day = cacher.GetDay(date);
            }

            if (day == null)
                throw new Exception("Cache build failed");

            return day;
        }

        public async Task<BreadWeek> GetWeek(DateTime date)
        {
            BreadWeek? week = cacher.GetWeek(date);
            if (week == null)
                week = await QuickBuildCacheWeek(date);
            return week;
        }

    }
}

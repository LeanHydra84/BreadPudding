using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BreadPudding;
using BreadPudding.Caching;
using BreadPuddingCalendar.Util;

namespace BreadPuddingCalendar
{
    internal class CalendarLoader
    {
        public MenuParser Parser { get; }
        private BreadCacheManager cacheManager;

        public Calendar MenuCalendar { get; }

        public CalendarLoader(BreadCacheManager cacheManager, Calendar set)
        {
            Parser = new MenuParser();
            MenuCalendar = set;
            this.cacheManager = cacheManager;

            cacheManager.OnDayUpdated += UpdateCell;
        }

        private int GetCellByTime(DateTime date)
        {
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            int leftPad = (MenuCalendar.Cells.Count - daysInMonth - 1) / 2;
            return date.Day + leftPad;
        }

        private void SetCellContents(CalendarCell cell, BreadDay day)
        {
            cell.Border.Background = MenuCalendar.ColorResource.CellPopulated;
        }

        private void UpdateCell(DateTime date)
        {
            //BreadDay? bread = cacheManager.Cacher.GetDay(date);
            //if(bread == null)
            //    throw new Exception("Cache Null Return");

            //int cell = GetCellByTime(date);
            //SetCellContents(Resources.Cells[cell], bread);
            
            //dispatchedThreads--;
            //if(dispatchedThreads == 0)
            //{
            //    // stop spinning animation?
            //}
        }

        //private async Task LoadAsyncDate(DateTime firstOfMonth)
        //{
        //    int daysInMonth = DateTime.DaysInMonth(firstOfMonth.Year, firstOfMonth.Month);
        //    for (int i = 0; i < daysInMonth; i += 7)
        //    {
        //        DateTime thisWeek = firstOfMonth.AddDays(i);
        //        Trace.WriteLine($"Loading Week: {thisWeek.ToString("MM-dd-yyyy")}");
        //        await cacheManager.GetWeek(thisWeek);
        //        //await cacheManager.QuickBuildCacheWeek(thisWeek);
        //        dispatchedThreads++;
        //    }
        //}

        private async Task LoadAsyncDate(DateTime firstOfMonth)
        {
            MenuCalendar.LoadedData.Clear();
            int daysInMonth = DateTime.DaysInMonth(firstOfMonth.Year, firstOfMonth.Month);
            for(int i = 0; i < daysInMonth; i++)
            {
                DateTime date = firstOfMonth.AddDays(i).Date;
                //Console.WriteLine($"Loading date {date}");
                BreadDay day = await cacheManager.GetDay(date);

                var cell = MenuCalendar.Cells[GetCellByTime(date)];
                cell.Border.Background = MenuCalendar.ColorResource.CellPopulated;

                Food[] parsedData = Parser.ParseFood(day.Data, FoodData.All);
                MenuCalendar.LoadedData.Add(parsedData);
                if (parsedData.Length > 0) cell.Circle.Visibility = Visibility.Visible;
            }
        }

        public void LoadMonth(DateTime firstOfTheMonth) // dispatches asynchronous loaders
        {
            _ = LoadAsyncDate(firstOfTheMonth);
        }

        //public void LoadCachedMonth(DateTime firstOfTheMonth) // only loads cached data
        //{
        //    MenuCalendar.LoadedData.Clear();
        //    int daysInMonth = DateTime.DaysInMonth(firstOfTheMonth.Year, firstOfTheMonth.Month);
        //    for (int i = 0; i < daysInMonth; i++)
        //    {
        //        DateTime date = firstOfTheMonth.AddDays(i).Date;
        //        BreadDay? day = cacheManager.Cacher.GetDay(date);
                
        //        if(day == null)
        //        {
        //            MenuCalendar.LoadedData.Add(null);
        //            continue;
        //        }

        //        var cell = MenuCalendar.Cells[GetCellByTime(date)];
        //        cell.Border.Background = MenuCalendar.ColorResource.CellPopulated;

        //        Food[] parsedData = Parser.ParseFood(day.Data, FoodData.All);
        //        MenuCalendar.LoadedData.Add(parsedData);
        //        if (parsedData.Length > 0) cell.Circle.Visibility = Visibility.Visible;

        //    }
        //}
    }
}

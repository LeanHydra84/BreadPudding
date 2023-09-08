using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BreadPudding.caching;
using BreadPudding.Caching;
using Newtonsoft.Json;

namespace BreadPudding
{
    public class BreadCacher
    {

        public static string DateFormat = "MM-dd-yyyy";

        private BreadWeek? mostRecentWeek;

        public string Path { get; }

        public BreadCacher(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    throw new Exception("Failed to create cache directory");
                }
            }
            Path = path;
        }

        public bool WeekExists(DateTime date)
        {
            DateTime adjusted = EnsureWeekStart(date);
            string fp = GetFilePath(adjusted);
            return File.Exists(fp);
        }

        public bool DayExists(DateTime date)
        {
            DateTime adjusted = EnsureWeekStart(date);
            BreadWeek? week = GetWeek(adjusted);
            if (week == null) return false;
            int wIndex = (date.Date - week.Date).Days;
            return week.Days[wIndex] != null;
        }

        public void GarbageCollection(int weeksThreshold = 1) // deletes entries more than a week in the past
        {
            IEnumerable<string> files = System.IO.Directory.EnumerateFiles(Path);
            foreach(string fn in files)
            {
                BreadPsuedoWeek? psuedo = JsonConvert.DeserializeObject<BreadPsuedoWeek>(File.ReadAllText(fn));
                if(psuedo == null)
                {
                    File.Delete(fn);
                    continue;
                }

                int age = (DateTime.Now.Date - psuedo.Date).Days;
                if(age > weeksThreshold * 7)
                {
                    File.Delete(fn);
                }
            }
        }

        public void Cache(BreadDay day)
        {
            BreadWeek? week = GetWeek(day.Date);
            if(week == null) week = new BreadWeek(day.Date); // constructor ensures weekdate correctness

            int replaceIndex = (day.Date - week.Date).Days;
            week.Days[replaceIndex] = day;

            CacheWeek(week, true);
        }

        public BreadWeek MergeWeeks(BreadWeek left, BreadWeek? right)
        {
            if (right == null) return left;
            BreadWeek newWeek = new BreadWeek(left.Date);
            for (int i = 0; i < 7; i++)
            {
                newWeek.Days[i] = left.Days[i] ?? right?.Days[i];
            }
            return newWeek;
        }

        public void CacheWeek(BreadWeek week, bool overwrite = true)
        {
            string fn = GetFilePath(week.Date);
            if(!overwrite)
            {
                BreadWeek? oldWeek = GetWeek(week.Date);
                week = MergeWeeks(week, oldWeek);
            }

            mostRecentWeek = week;
            string json = JsonConvert.SerializeObject(week);
            File.WriteAllText(fn, json);
        }

        public BreadWeek? GetWeek(DateTime date)
        {
            DateTime ensuredWeekDate = EnsureWeekStart(date);
            if (ensuredWeekDate == mostRecentWeek?.Date)
                return mostRecentWeek;

            string fn = GetFilePath(ensuredWeekDate);

            if (!File.Exists(fn)) return null;

            string asJson = File.ReadAllText(fn);
            BreadWeek? week = JsonConvert.DeserializeObject<BreadWeek>(asJson);
            mostRecentWeek = week;
            return week;
        }

        public BreadDay? GetDay(DateTime date)
        {
            BreadWeek? week = GetWeek(date);
            if (week == null) return null;
            int wIndex = (date.Date - week.Date).Days;
            return week.Days[wIndex];
        }

        public string GetFilePath(DateTime dt)
        {
            return System.IO.Path.Combine(Path, GetFileName(dt));
        }

        public static string GetFileName(DateTime dt)
        {
            return dt.ToString(DateFormat) + ".json";
        }

        public static DateTime EnsureWeekStart(DateTime dt)
        {

            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-diff).Date;
        }


    }
}

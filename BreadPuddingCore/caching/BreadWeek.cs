using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BreadPudding.Caching
{
    public class BreadWeek
    {

        public BreadWeek(DateTime date) : this()
        {
            Date = BreadCacher.EnsureWeekStart(date);
        }

        public BreadWeek()
        {
            Days = new BreadDay[7];
        }

        [JsonProperty("date")] public DateTime Date { get; set; }
        [JsonProperty("days")] public BreadDay?[] Days { get; set; }

        public override string ToString() => BreadCacher.GetFileName(Date);

    }
}

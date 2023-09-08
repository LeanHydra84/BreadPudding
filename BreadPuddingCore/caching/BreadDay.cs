using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BreadPudding.Caching
{
    public class BreadDay
    {

        public BreadDay(DateTime date, Food[] data)
        {
            Date = date.Date;
            Data = data;
        }

        public BreadDay(DateTime date)
        {
            Date = date;
            Data = new Food[0];
        }

        public BreadDay()
        {
            Date = DateTime.Now.Date;
            Data = new Food[0];
        }

        [JsonProperty("date")] public DateTime Date { get; set; }
        [JsonProperty("data")] public Food[] Data { get; set; }

        public override string ToString() => Date.ToString(BreadCacher.DateFormat);

    }
}

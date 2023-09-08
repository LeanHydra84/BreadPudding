using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadPudding.caching
{
    // class just used for metadata checking for date of weekfile
    internal class BreadPsuedoWeek
    {
        [JsonProperty("date")] public DateTime Date { get; set; }
        public override string ToString() => BreadCacher.GetFileName(Date);
    }
}

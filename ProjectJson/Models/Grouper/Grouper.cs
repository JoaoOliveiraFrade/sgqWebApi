using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class Grouper
    {
        public Int32 id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string executiveSummary { get; set; }

        public string trafficLight { get; set; }

        public DateTime startTiUat { get; set; }
        public DateTime endTiUat { get; set; }

        public DateTime startTRG { get; set; }
        public DateTime endTRG { get; set; }

        public DateTime startStabilization { get; set; }
        public DateTime endStabilization { get; set; }
    }
}
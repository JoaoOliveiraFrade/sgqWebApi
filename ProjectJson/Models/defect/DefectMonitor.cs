using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
	public class DefectMonitor
    {
        public string subproject { get; set; }
        public string delivery { get; set; }
        public int id { get; set; }
        public string trafficLight { get; set; }
        public string provider { get; set; }
        public string testManufDefect { get; set; }
        public string subDel { get; set; }
        public string system { get; set; }
        public string queue { get; set; }
        public string origin { get; set; }
        public string status { get; set; }
        public string severity { get; set; }
        public double agingHours { get; set; }
        public double timeLastQueueHours { get; set; }
        public int qtyImpactCT { get; set; }
        public int pingPong { get; set; }
    }
}
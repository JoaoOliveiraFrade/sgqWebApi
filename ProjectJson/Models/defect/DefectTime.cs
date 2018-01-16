using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class DefectTime
    {
        public string subproject { get; set; }
        public string delivery { get; set; }
        public int defect { get; set; }
        public string queue { get; set; }
        public string status { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public double workTime { get; set; }
    }
}


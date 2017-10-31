using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class DefectInsideSLA
    {
        public string month { get; set; }
        public string year { get; set; }
        public string devManuf { get; set; }
        public string system { get; set; }
        public string subprojectDelivery { get; set; }
        public string severity { get; set; }

        public int qtyDefect { get; set; }
        public int qtyInsideSLA { get; set; }
        public double percInsideSLA { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class DefectDensity
    {
        public string month { get; set; }
        public string year { get; set; }
        public string devManuf { get; set; }
        public string system { get; set; }
        public string subDel { get; set; }

        public int qtyDefect { get; set; }
        public int qtyCt { get; set; }
        public double density { get; set; }
    }
}
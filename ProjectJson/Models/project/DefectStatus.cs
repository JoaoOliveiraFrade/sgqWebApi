using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class DefectStatus
    {
        public string name { get; set; }
        public int qtyDefects { get; set; }
        public int totalDefects { get; set; }
        public double percent { get; set; }
    }
}
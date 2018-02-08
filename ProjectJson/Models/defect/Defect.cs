using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class Defect
    {
        public string subproject { get; set; }
        public string delivery { get; set; }
        public string subDel { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string inFactoryType { get; set; }
        public string queue { get; set; }
        public string defectSystem { get; set; }
        public string severity { get; set; }
        public double aging { get; set; }
        public string agingFormat { get; set; }
        public int pingPong { get; set; }
        public string trgSystem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class defectsOpen
    {
        public string subDel { get; set; }
        public string subproject { get; set; }
        public string delivery { get; set; }
        public int defect { get; set; }
        public string status { get; set; }
        public string queue { get; set; }
        public string defectSystem { get; set; }
        public string severity { get; set; }
        public double aging { get; set; }
        public int pingPong { get; set; }
    }
}

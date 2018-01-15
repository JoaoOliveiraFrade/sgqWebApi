using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models {
    public class DefectReopened {
        public string month { get; set; }
        public string year { get; set; }
        public string devManuf { get; set; }
        public string system { get; set; }
        public string subDel { get; set; }
        public int qtyDefect { get; set; }
        public int qtyReopened { get; set; }
        public double percReopened { get; set; }
    }
}

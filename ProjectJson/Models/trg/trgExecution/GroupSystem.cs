using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class GroupSystem
    {
        public string system { get; set; }

        public int active { get; set; }

        public int planned { get; set; }

        public int realized { get; set; }

        public double GAP { get; set; }

        public string percGAP { get; set; }


        public int plannedAcum { get; set; }

        public int realizedAcum { get; set; }

        public double GAPAcum { get; set; }


        public string percGAPAcum { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models.Project
{
    public class CtImpactedXDefects
    {
        public string date { get; set; }

        public int qtyDefectsAmb { get; set; }
        public int qtyDefectsCons { get; set; }
        public int qtyDefectsTot { get; set; }
        public int qtyCtsImpacted { get; set; }

        public int qtyDefectsAmbAcum { get; set; }
        public int qtyDefectsConsAcum { get; set; }
        public int qtyDefectsTotAcum { get; set; }
        public int qtyCtsImpactedAcum { get; set; }
    }
}
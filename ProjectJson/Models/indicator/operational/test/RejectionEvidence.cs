using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class RejectionEvidence
    {
        public string month { get; set; }
        public string year { get; set; }
        public string testManuf { get; set; }
        public string system { get; set; }
        public string subDel { get; set; }

        public int qtyEvidence { get; set; }
        public int qtyEvidenceClient { get; set; }

        public int qtyRejectionTechnique { get; set; }
        public int qtyRejectionClient { get; set; }
        public int qtyRejectionTotal { get; set; }
    }
}
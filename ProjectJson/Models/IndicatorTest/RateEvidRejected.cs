using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class RateEvidRejected
    {
		public string testManuf { get; set; }
		public string system { get; set; }
		public string subprojectDelivery { get; set; }

        public int evidences { get; set; }
        public int evidencesClient { get; set; }

        public int rejectionsTechnique { get; set; }
		public int rejectionsClient { get; set; }
		public int rejectionsTotal { get; set; }
	}
}
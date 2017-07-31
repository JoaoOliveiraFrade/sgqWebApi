using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class RateEvidRejected
	{
		public string monthYear { get; set; }
		public string yearMonth { get; set; }
		public string testManuf { get; set; }
		public string system { get; set; }
		public string subprojectDelivery { get; set; }

        public int tiEvidences { get; set; }
        public int tiRejections { get; set; }

        public int uatEvidences { get; set; }
		public int uatRejections { get; set; }

		public int totalEvidences { get; set; }
		public int totalRejections { get; set; }
	}
}
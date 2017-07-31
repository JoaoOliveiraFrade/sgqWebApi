using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class Produtivity
    {
		public string monthYear { get; set; }
		public string yearMonth { get; set; }
		public string testManuf { get; set; }
		public string system { get; set; }
		public string subprojectDelivery { get; set; }
        public int passed { get; set; }
        public int failed { get; set; }
        public int productivity { get; set; }
	}
}
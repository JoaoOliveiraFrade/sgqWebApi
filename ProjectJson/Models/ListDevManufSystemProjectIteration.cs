using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
	public class ListDevManufSystemProjectIteration {
		public List<string> selectedDevManufs { get; set; }
		public List<string> selectedSystems { get; set; }
		public List<string> selectedProjects { get; set; }
        public List<string> iterations { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
	public class DevManufSystemProjectIteration {
		public List<string> selectedDevManuf { get; set; }
		public List<string> selectedSystem { get; set; }
		public List<string> selectedProject { get; set; }
        public List<string> iterations { get; set; }
    }
}

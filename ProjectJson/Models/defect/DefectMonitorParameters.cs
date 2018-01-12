using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
	public class DefectMonitorParameters
    {
		public List<string> selectedQueue { get; set; }
		public List<string> selectedStatus { get; set; }
        public List<string> selectedTrafficLight { get; set; }
        public List<string> selectedProject { get; set; }
    }
}
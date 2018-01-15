using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
	public class DefectMonitorParameter
    {
		public List<string> selectedDefectQueue { get; set; }
        public List<string> selectedDefectStatus { get; set; }
        public List<string> selectedDefectTrafficLight { get; set; }
        public List<string> selectedProject { get; set; }
    }
}
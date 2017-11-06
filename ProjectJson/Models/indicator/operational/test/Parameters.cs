using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
	public class Parameters
    {
		public List<string> selectedTestManufs { get; set; }
		public List<string> selectedSystem { get; set; }
		public List<string> selectedProject { get; set; }
    }
}
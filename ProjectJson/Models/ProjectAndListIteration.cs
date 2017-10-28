using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
	public class ProjectAndListIteration {
		public string subproject { get; set; }
		public string delivery { get; set; }
        public List<string> iterations { get; set; }
    }
}

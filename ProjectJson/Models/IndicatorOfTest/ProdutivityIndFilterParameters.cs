using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
	public class ProdutivityFilterParameters
	{
		public List<string> selectedTestManufs { get; set; }
		public List<string> selectedSystems { get; set; }
		public List<string> selectedProjects { get; set; }
	}
}
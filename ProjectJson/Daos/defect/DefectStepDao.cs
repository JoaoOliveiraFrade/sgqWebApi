using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
	public class DefectStepDao
    {
		private Connection connection;

		public DefectStepDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

		public IList<DefectStep> FbyProject(string subproject, string delivery)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\defect\defectStep\fbyProject.sql"), Encoding.Default);
			var list = connection.Executar<DefectStep>(sql);
			return list;
		}
    }
}

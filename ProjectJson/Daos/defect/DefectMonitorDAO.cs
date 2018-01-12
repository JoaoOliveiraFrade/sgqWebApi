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
	public class DefectMonitorDao
    {
		private Connection connection;

		public DefectMonitorDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

		public IList<IdName> FbyQueueStatusTrafficLightProject(DefectMonitorParameters parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\defect\defectMonitor\fbyQueueStatusTrafficLightProject.sql"), Encoding.Default);
			var list = connection.Executar<IdName>(sql);
			return list;
		}
    }
}

using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.DAOs
{
	public class DefectMonitorDAO
    {
		private Connection _connection;

		public DefectMonitorDAO()
		{
			_connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			_connection.Dispose();
		}

		public IList<IdName> defectAqueue(Parameters parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\defectQueue\all.sql"), Encoding.Default);
			var list = _connection.Executar<IdName>(sql);
			return list;
		}
    }
}

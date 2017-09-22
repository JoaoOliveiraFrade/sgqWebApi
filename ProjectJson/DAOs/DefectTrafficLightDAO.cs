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
	public class DefectTrafficLightDAO
    {
		private Connection _connection;

		public DefectTrafficLightDAO()
		{
			_connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			_connection.Dispose();
		}

		public IList<IdName> All()
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\defectTrafficLight\all.sql"), Encoding.Default);
			var list = _connection.Executar<IdName>(sql);
			return list;
		}
    }
}
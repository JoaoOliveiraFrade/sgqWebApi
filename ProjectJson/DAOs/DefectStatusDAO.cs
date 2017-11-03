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
	public class DefectStatusDao
    {
		private Connection connection;

		public DefectStatusDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

		public IList<IdName> All()
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\defectStatus\all.sql"), Encoding.Default);
			var list = connection.Executar<IdName>(sql);
			return list;
		}
    }
}
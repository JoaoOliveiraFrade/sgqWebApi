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
	public class DefectQueueDao
    {
		private Connection connection;

		public DefectQueueDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

		public IList<IdName> Data()
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\defect\defectQueue\data.sql"), Encoding.Default);
			var list = connection.Executar<IdName>(sql);
			return list;
		}
    }
}
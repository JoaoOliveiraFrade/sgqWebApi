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
	public class AgileCentralDao {
		private Connection connection;

		public AgileCentralDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

        public IList<AgileCentral> Data(SubprojectDelivery subprojectDelivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\agileCentral\data.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subprojectDelivery.subproject);
            sql = sql.Replace("@delivery", subprojectDelivery.delivery);
            return connection.Executar<AgileCentral>(sql);
        }

        public AgileCentral Detail(SubprojectDelivery subprojectDelivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\agileCentral\detail.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subprojectDelivery.subproject);
            sql = sql.Replace("@delivery", subprojectDelivery.delivery);
            return connection.Executar<AgileCentral>(sql)[0];
        }

    }
}

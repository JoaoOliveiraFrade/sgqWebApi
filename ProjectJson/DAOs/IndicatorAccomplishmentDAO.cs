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
	public class IndicatorAccomplishmentDAO
    {
		private Connection connection;

		public IndicatorAccomplishmentDAO()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

        public IList<defectInsideSla> defectInsideSlaFbyListTestManufSystemProject(ListDevManufSystemProject parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorAccomplishment\defectInsideSlaFbyListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var result = connection.Executar<defectInsideSla>(sql);
            return result;
        }
    }
}
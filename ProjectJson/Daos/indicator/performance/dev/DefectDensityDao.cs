using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos.Ind.Perf.Dev
{
	public class DefectDensityDao
    {
		private Connection connection;

		public DefectDensityDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

        public IList<DefectDensity> data(DevManufSystemProject parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\performance\Dev\defectDensity\data.sql"), Encoding.Default);
            sql = sql.Replace("@selectedDevManuf", "'" + string.Join("','", parameters.selectedDevManuf) + "'");
            sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
            sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
            var result = connection.Executar<DefectDensity>(sql);
            return result;
        }

        public IList<DefectDensity> dataFbyProject(string subproject, string delivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\performance\Dev\defectDensity\dataFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            var result = connection.Executar<DefectDensity>(sql);
            return result;
        }
    }
}
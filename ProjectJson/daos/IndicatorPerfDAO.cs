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
	public class IndicatorPerfDAO
	{
		private Connection _connection;

		public IndicatorPerfDAO()
		{
			_connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			_connection.Dispose();
		}

        public IList<DefectDensity> defectDensity_fbyListDevManufSystemProject(Parameters2 parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorPerf\defectDensity_fbyListDevManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var result = _connection.Executar<DefectDensity>(sql);
            return result;
        }

        public IList<defectOfTSInTI> defectOfTSInTI_fbyListDevManufSystemProject(Parameters2 parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorPerf\defectOfTSInTI_fbyListDevManufSystemProject.sql"), Encoding.Default);
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			var list = _connection.Executar<defectOfTSInTI>(sql);
			return list;
		}
    }
}
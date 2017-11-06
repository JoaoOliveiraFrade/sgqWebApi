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
	public class DefectOfTSInTIAgentDao
    {
		private Connection connection;

		public DefectOfTSInTIAgentDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

        public IList<DefectOfTSInTI> data(DevManufSystemProject parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\performance\Dev\defectOfTSInTIAgent\data.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedDevManuf) + "'");
            sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
            sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
            var list = connection.Executar<DefectOfTSInTI>(sql);
            return list;
        }

        public IList<DefectOfTSInTI> dataFbyProject(string subproject, string delivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\performance\Dev\defectOfTSInTIAgent\dataFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            var result = connection.Executar<DefectOfTSInTI>(sql);
            return result;
        }
    }
}
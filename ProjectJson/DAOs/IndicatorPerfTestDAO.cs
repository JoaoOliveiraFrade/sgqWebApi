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
	public class IndicatorPerfTestDAO
	{
		private Connection connection;

		public IndicatorPerfTestDAO()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

  //      public IList<DefectDensity> defectDensityFbyProject(string subproject, string delivery) {
  //          string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfTest\defectDensityFbyProject.sql"), Encoding.Default);
  //          sql = sql.Replace("@subproject", subproject);
  //          sql = sql.Replace("@delivery", delivery);
  //          var result = connection.Executar<DefectDensity>(sql);
  //          return result;
  //      }

  //      public IList<DefectDensity> defectDensityFbyListDevManufSystemProject(ListDevManufSystemProject parameters) {
  //          string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfTest\defectDensityFbyListDevManufSystemProject.sql"), Encoding.Default);
  //          sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
  //          sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
  //          sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
  //          var result = connection.Executar<DefectDensity>(sql);
  //          return result;
  //      }

  //      public IList<defectOfTSInTI> defectOfTSInTI_fbyListDevManufSystemProject(ListDevManufSystemProject parameters)
		//{
		//	string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfTest\defectOfTSInTI_fbyListDevManufSystemProject.sql"), Encoding.Default);
		//	sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
		//	sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
		//	sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
		//	var list = connection.Executar<defectOfTSInTI>(sql);
		//	return list;
		//}
    }
}
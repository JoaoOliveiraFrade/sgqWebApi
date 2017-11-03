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
	public class IndPerfTestDao
	{
		private Connection connection;

		public IndPerfTestDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

  //      public IList<DefectDensity> defectDensityFbyProject(string subproject, string delivery) {
  //          string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indPerfTest\defectDensityFbyProject.sql"), Encoding.Default);
  //          sql = sql.Replace("@subproject", subproject);
  //          sql = sql.Replace("@delivery", delivery);
  //          var result = connection.Executar<DefectDensity>(sql);
  //          return result;
  //      }

  //      public IList<DefectDensity> defectDensityFbydevManufsystemProject(devManufsystemProject parameters) {
  //          string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indPerfTest\defectDensityFbydevManufsystemProject.sql"), Encoding.Default);
  //          sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
  //          sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
  //          sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
  //          var result = connection.Executar<DefectDensity>(sql);
  //          return result;
  //      }

  //      public IList<defectOfTSInTI> defectOfTSInTI_fbydevManufsystemProject(devManufsystemProject parameters)
		//{
		//	string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indPerfTest\defectOfTSInTI_fbydevManufsystemProject.sql"), Encoding.Default);
		//	sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
		//	sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
		//	sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
		//	var list = connection.Executar<defectOfTSInTI>(sql);
		//	return list;
		//}
    }
}
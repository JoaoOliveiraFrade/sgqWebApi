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

        //public IList<DefectDensity> defectDensityFbydevManufsystemProject(devManufsystemProject parameters)
        //{
        //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\peformance\test\defectDensityFbydevManufsystemProject.sql"), Encoding.Default);
        //    sql = sql.Replace("@selectedDevManuf", "'" + string.Join("','", parameters.selectedDevManuf) + "'");
        //    sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
        //    sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
        //    var result = connection.Executar<DefectDensity>(sql);
        //    return result;
        //}

        //public IList<defectOfTSInTI> defectOfTSInTI_fbydevManufsystemProject(devManufsystemProject parameters)
        //{
        //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\peformance\test\defectOfTSInTI_fbydevManufsystemProject.sql"), Encoding.Default);
        //    sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedDevManuf) + "'");
        //    sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
        //    sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
        //    var list = connection.Executar<defectOfTSInTI>(sql);
        //    return list;
        //}

        //public IList<DefectDensity> defectDensityFbyProject(string subproject, string delivery)
        //{
        //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\peformance\test\defectDensityFbyProject.sql"), Encoding.Default);
        //    sql = sql.Replace("@subproject", subproject);
        //    sql = sql.Replace("@delivery", delivery);
        //    var result = connection.Executar<DefectDensity>(sql);
        //    return result;
        //}
    }
}
using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace ProjectWebApi.DAOs
{
	public class IndicatorTestDAO
	{
		private Connection _connection;

		public IndicatorTestDAO()
		{
			_connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			_connection.Dispose();
		}

		//public IList<Produtivity> getProductivityByProject(string subproject, string delivery)
		//{
		//	string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\ProdutivityByProject.sql"));
		//	sql = sql.Replace("@subproject", subproject);
		//	sql = sql.Replace("@delivery", delivery);
		//	var list = _connection.Executar<Produtivity>(sql);
		//	return list;
		//}

		public IList<Produtivity> getProdutivityByListTestManufSystemProject(Parameters parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\IndicatorTest\ProdutivityByListTestManufSystemProject.sql"));
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			var list = _connection.Executar<Produtivity>(sql);
			var x = 1 * 2;
			Console.WriteLine(x);
			return list;
		}

		public IList<RateEvidRejected> getRateEvidRejectedByListTestManufSystemProject(Parameters parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\IndicatorTest\RateEvidRejectedByListTestManufSystemProject.sql"));
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			var list = _connection.Executar<RateEvidRejected>(sql);
            return list;
		}

        public IList<RateEvidRejectedGroupTimeline> getRateEvidRejectedByListTestManufSystemProjectGroupTimeline(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\IndicatorTest\RateEvidRejectedByListTestManufSystemProjectGroupTimeline.sql"));
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<RateEvidRejectedGroupTimeline>(sql);

            return list;
        }

        public IList<RateDefectUnfounded> getRateDefectUnfoundedByListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\IndicatorTest\RateDefectUnfoundedByListTestManufSystemProject.sql"));
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<RateDefectUnfounded>(sql);

            return list;
        }
        public IList<RateDefectUat> getRateDefectUatByListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\IndicatorTest\RateDefectUatByListTestManufSystemProject.sql"));
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<RateDefectUat>(sql);

            return list;
        }
        public IList<averangeRetestHours> getAverangeRetestHoursListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\IndicatorTest\AverangeRetestHoursByListTestManufSystemProject.sql"));
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<averangeRetestHours>(sql);

            return list;
        }

    }
}
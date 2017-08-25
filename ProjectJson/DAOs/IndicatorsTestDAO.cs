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

		public IList<Produtivity> getProductivityByProject(string subproject, string delivery)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\ProdutivityByProject.sql"));
			sql = sql.Replace("@subproject", subproject);
			sql = sql.Replace("@delivery", delivery);
			var list = _connection.Executar<Produtivity>(sql);
			return list;
		}

		public IList<Produtivity> getProdutivityByListTestManufSystemProject(ProdutivityFilterParameters ProdutivityFilterParameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\Indicator\ProdutivityByListTestManufSystemProject.sql"));
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", ProdutivityFilterParameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", ProdutivityFilterParameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", ProdutivityFilterParameters.selectedProjects) + "'");
			var list = _connection.Executar<Produtivity>(sql);
			var x = 1 * 2;
			Console.WriteLine(x);
			return list;
		}

		public IList<RateEvidRejected> getRateEvidRejectedByListTestManufSystemProject(RateEvidRejectedParameters parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\Indicator\RateEvidRejectedByListTestManufSystemProject.sql"));
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			var list = _connection.Executar<RateEvidRejected>(sql);
            return list;
		}

        public IList<RateEvidRejectedGroupTimeline> getRateEvidRejectedByListTestManufSystemProjectGroupTimeline(RateEvidRejectedParameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\Indicator\RateEvidRejectedByListTestManufSystemProjectGroupTimeline.sql"));
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<RateEvidRejectedGroupTimeline>(sql);

            return list;
        }

    }
}
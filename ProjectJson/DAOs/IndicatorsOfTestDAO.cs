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
	public class IndicatorOfTestDAO
	{
		private Connection _connection;

		public IndicatorOfTestDAO()
		{
			_connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			_connection.Dispose();
		}

		public IList<ProdutivityInd> getProductivityByProject(string subproject, string delivery)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\ProdutivityIndByProject.sql"));
			sql = sql.Replace("@subproject", subproject);
			sql = sql.Replace("@delivery", delivery);
			var list = _connection.Executar<ProdutivityInd>(sql);
			return list;
		}

		public IList<ProdutivityInd> getProdutivityIndByListTestManufSystemProject(ProdutivityIndFilterParameters ProdutivityIndFilterParameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\ProdutivityIndByListTestManufSystemProject.sql"));
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", ProdutivityIndFilterParameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", ProdutivityIndFilterParameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", ProdutivityIndFilterParameters.selectedProjects) + "'");
			var list = _connection.Executar<ProdutivityInd>(sql);
			var x = 1 * 2;
			Console.WriteLine(x);
			return list;
		}

		public IList<RateRejectionEvidenceInd> getRateRejectionEvidenceIndByListTestManufSystemProject(ProdutivityIndFilterParameters ProdutivityIndFilterParameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\RateRejectionEvidenceIndByListTestManufSystemProject.sql"));
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", ProdutivityIndFilterParameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", ProdutivityIndFilterParameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", ProdutivityIndFilterParameters.selectedProjects) + "'");
			var list = _connection.Executar<RateRejectionEvidenceInd>(sql);
			return list;
		}
	}
}
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

		//public IList<productivity> getProductivityByProject(string subproject, string delivery)
		//{
		//	string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\productivityByProject.sql"), Encoding.Default);
		//	sql = sql.Replace("@subproject", subproject);
		//	sql = sql.Replace("@delivery", delivery);
		//	var list = _connection.Executar<productivity>(sql);
		//	return list;
		//}

		public IList<Productivity> productivityByListTestManufSystemProject(Parameters parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorTest\productivityByListTestManufSystemProject.sql"), Encoding.Default);
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			var list = _connection.Executar<Productivity>(sql);
			return list;
		}

		public IList<RateEvidRejected> rateEvidRejectedByListTestManufSystemProject(Parameters parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorTest\rateEvidRejectedByListTestManufSystemProject.sql"), Encoding.Default);
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			var list = _connection.Executar<RateEvidRejected>(sql);
            return list;
		}

        public IList<RateEvidRejectedGroupTimeline> rateEvidRejectedByListTestManufSystemProjectGroupTimeline(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorTest\rateEvidRejectedByListTestManufSystemProjectGroupTimeline.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<RateEvidRejectedGroupTimeline>(sql);

            return list;
        }

        public IList<RateDefectUnfounded> rateDefectUnfoundedByListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorTest\rateDefectUnfoundedByListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<RateDefectUnfounded>(sql);

            return list;
        }
        public IList<RateDefectUat> rateDefectUatByListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorTest\rateDefectUatByListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<RateDefectUat>(sql);

            return list;
        }
        public IList<AverangeRetestHours> averangeRetestHoursByListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorTest\averangeRetestHoursByListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = _connection.Executar<AverangeRetestHours>(sql);

            return list;
        }

    }
}
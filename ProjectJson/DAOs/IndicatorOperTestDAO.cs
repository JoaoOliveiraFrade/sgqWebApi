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
	public class IndicatorOperTestDAO
	{
		private Connection connection;

		public IndicatorOperTestDAO()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

        #region Productivity

            public IList<Productivity> productivityFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperTest\productivityFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<Productivity>(sql);
                return result;
            }


            //public IList<productivity> getProductivityByProject(string subproject, string delivery)
            //{
            //	string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\productivityByProject.sql"), Encoding.Default);
            //	sql = sql.Replace("@subproject", subproject);
            //	sql = sql.Replace("@delivery", delivery);
            //	var list = connection.Executar<productivity>(sql);
            //	return list;
            //}

            public IList<Productivity> productivityByListTestManufSystemProject(Parameters parameters)
		    {
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\productivityByListTestManufSystemProject.sql"), Encoding.Default);
			    sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			    sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			    sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			    var list = connection.Executar<Productivity>(sql);
			    return list;
		    }

        #endregion

        public IList<evidRejected> evidRejectedByListTestManufSystemProject(Parameters parameters)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\evidRejectedByListTestManufSystemProject.sql"), Encoding.Default);
			sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			var list = connection.Executar<evidRejected>(sql);
            return list;
		}

        public IList<evidRejectedGroupTimeline> evidRejectedByListTestManufSystemProjectGroupTimeline(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\evidRejectedByListTestManufSystemProjectGroupTimeline.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = connection.Executar<evidRejectedGroupTimeline>(sql);

            return list;
        }

        public IList<RateDefectUnfounded> rateDefectUnfoundedByListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\rateDefectUnfoundedByListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = connection.Executar<RateDefectUnfounded>(sql);

            return list;
        }
        public IList<defectUat> defectUatByListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\defectUatByListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = connection.Executar<defectUat>(sql);

            return list;
        }
        public IList<AverangeRetestHours> averangeRetestHoursByListTestManufSystemProject(Parameters parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\averangeRetestHoursByListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = connection.Executar<AverangeRetestHours>(sql);

            return list;
        }

    }
}
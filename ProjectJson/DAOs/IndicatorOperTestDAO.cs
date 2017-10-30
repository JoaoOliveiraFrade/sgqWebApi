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


        #region rejectionEvidence

            public IList<RejectionEvidence> rejectionEvidenceFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperTest\rejectionEvidenceFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<RejectionEvidence>(sql);
                return result;
            }

            public IList<RejectionEvidence> rejectionEvidenceByListTestManufSystemProject(Parameters parameters)
		    {
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\rejectionEvidenceByListTestManufSystemProject.sql"), Encoding.Default);
			    sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			    sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			    sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			    var list = connection.Executar<RejectionEvidence>(sql);
                return list;
		    }

            //public IList<rejectionEvidenceGroupTimeline> rejectionEvidenceByListTestManufSystemProjectGroupTimeline(Parameters parameters)
            //{
            //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\rejectionEvidenceByListTestManufSystemProjectGroupTimeline.sql"), Encoding.Default);
            //    sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            //    sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            //    sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            //    var list = connection.Executar<rejectionEvidenceGroupTimeline>(sql);

            //    return list;
            //}

        #endregion


        #region DefectUnfounded

            public IList<DefectUnfounded> defectUnfoundedFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperTest\defectUnfoundedFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectUnfounded>(sql);
                return result;
            }

            public IList<DefectUnfounded> defectUnfoundedFbyListTestManufSystemProject(Parameters parameters)
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\defectUnfoundedFbyListTestManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var list = connection.Executar<DefectUnfounded>(sql);

                return list;
            }

        #endregion


        #region DefectUAT

            public IList<DefectUAT> defectUATFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperTest\defectUATFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectUAT>(sql);
                return result;
            }

            public IList<DefectUAT> defectUATFbyListTestManufSystemProject(Parameters parameters)
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\defectUATFbyListTestManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var list = connection.Executar<DefectUAT>(sql);

                return list;
            }

        #endregion


        #region AverangeRetestHours

            public IList<AverangeRetestHours> averangeRetestHoursByListTestManufSystemProject(Parameters parameters)
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorOperTest\averangeRetestHoursByListTestManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var list = connection.Executar<AverangeRetestHours>(sql);

                return list;
            }

        #endregion

    }
}
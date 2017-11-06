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
	public class IndOperTestDao
	{
		private Connection connection;

		public IndOperTestDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

        #region Productivity

            public IList<Productivity> productivityFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\productivityFbyProject.sql"), Encoding.Default);
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
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\productivityByListTestManufSystemProject.sql"), Encoding.Default);
			    sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			    sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
			    sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
			    var list = connection.Executar<Productivity>(sql);
			    return list;
		    }

        #endregion


        #region rejectionEvidence

            public IList<RejectionEvidence> rejectionEvidenceFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\rejectionEvidenceFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<RejectionEvidence>(sql);
                return result;
            }

            public IList<RejectionEvidence> rejectionEvidenceByListTestManufSystemProject(Parameters parameters)
		    {
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\rejectionEvidenceByListTestManufSystemProject.sql"), Encoding.Default);
			    sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
			    sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
			    sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
			    var list = connection.Executar<RejectionEvidence>(sql);
                return list;
		    }

            //public IList<rejectionEvidenceGroupTimeline> rejectionEvidenceByListTestManufSystemProjectGroupTimeline(Parameters parameters)
            //{
            //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\rejectionEvidenceByListTestManufSystemProjectGroupTimeline.sql"), Encoding.Default);
            //    sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
            //    sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
            //    sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
            //    var list = connection.Executar<rejectionEvidenceGroupTimeline>(sql);

            //    return list;
            //}

        #endregion


        #region DefectUnfounded

            public IList<DefectUnfounded> defectUnfoundedFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\defectUnfoundedFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectUnfounded>(sql);
                return result;
            }

            public IList<DefectUnfounded> defectUnfoundedFbyListTestManufSystemProject(Parameters parameters)
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\defectUnfoundedFbyListTestManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
                sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
                sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
                var list = connection.Executar<DefectUnfounded>(sql);

                return list;
            }

        #endregion


        #region DefectUAT

            public IList<DefectUAT> defectUATFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\defectUATFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectUAT>(sql);
                return result;
            }

            public IList<DefectUAT> defectUATFbyListTestManufSystemProject(Parameters parameters)
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\defectUATFbyListTestManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
                sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
                sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
                var list = connection.Executar<DefectUAT>(sql);

                return list;
            }

        #endregion


        #region DefectAverangeRetestTime

            public IList<DefectAverangeRetestTime> defectAverangeRetestTimeFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\defectAverangeRetestTimeFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectAverangeRetestTime>(sql);
                return result;
            }


            public IList<DefectAverangeRetestTime> defectAverangeRetestTimeFbyListTestManufSystemProject(Parameters parameters)
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\test\defectAverangeRetestTimeFbyListTestManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedTestManufs) + "'");
                sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
                sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
                var list = connection.Executar<DefectAverangeRetestTime>(sql);

                return list;
            }

        #endregion

    }
}
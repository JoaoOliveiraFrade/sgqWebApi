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
	public class IndicatorPerfDevDAO
	{
		private Connection connection;

		public IndicatorPerfDevDAO()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}


        #region DefectDensity

            public IList<DefectDensity> defectDensityFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfDev\defectDensityFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectDensity>(sql);
                return result;
            }

            public IList<DefectDensity> defectDensityFbydevManufsystemProject(devManufsystemProject parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfDev\defectDensityFbydevManufsystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var result = connection.Executar<DefectDensity>(sql);
                return result;
            }

        #endregion


        #region DefectInsideSLA

            public IList<DefectInsideSLA> defectInsideSLAFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorPerfDev\defectInsideSLAFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectInsideSLA>(sql);
                return result;
            }

            public IList<DefectInsideSLA> defectInsideSLAFbyListTestManufSystemProject(devManufsystemProject parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorPerfDev\defectInsideSLAFbyListTestManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var result = connection.Executar<DefectInsideSLA>(sql);
                return result;
            }

        #endregion


        #region DefectOfTSInTI

        public IList<DefectOfTSInTI> defectOfTSInTIFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfDev\defectOfTSInTIFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectOfTSInTI>(sql);
                return result;
            }

            public IList<DefectOfTSInTI> defectOfTSInTIFbydevManufsystemProject(devManufsystemProject parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfDev\defectOfTSInTIFbydevManufsystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var list = connection.Executar<DefectOfTSInTI>(sql);
                return list;
            }

        #endregion


        #region DefectOfTSInTIAgent

        public IList<DefectOfTSInTI> defectOfTSInTIAgentFbyProject(string subproject, string delivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfDev\defectOfTSInTIAgentFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            var result = connection.Executar<DefectOfTSInTI>(sql);
            return result;
        }

        public IList<DefectOfTSInTI> defectOfTSInTIAgentFbydevManufsystemProject(devManufsystemProject parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfDev\defectOfTSInTIAgentFbydevManufsystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var list = connection.Executar<DefectOfTSInTI>(sql);
            return list;
        }

        #endregion

    }
}
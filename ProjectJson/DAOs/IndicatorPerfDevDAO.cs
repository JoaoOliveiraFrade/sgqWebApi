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

            public IList<DefectDensity> defectDensityFbyListDevManufSystemProject(ListDevManufSystemProject parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfDev\defectDensityFbyListDevManufSystemProject.sql"), Encoding.Default);
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

            public IList<DefectInsideSLA> defectInsideSLAFbyListTestManufSystemProject(ListDevManufSystemProject parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorPerfDev\defectInsideSLAFbyListTestManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var result = connection.Executar<DefectInsideSLA>(sql);
                return result;
            }

        #endregion


        #region DefectOfTSInTI

            public IList<defectOfTSInTI> defectOfTSInTI_fbyListDevManufSystemProject(ListDevManufSystemProject parameters)
		    {
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorPerfDev\defectOfTSInTI_fbyListDevManufSystemProject.sql"), Encoding.Default);
			    sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
			    sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			    sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			    var list = connection.Executar<defectOfTSInTI>(sql);
			    return list;
		    }

        #endregion
    }
}
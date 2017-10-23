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
	public class IndicatorAccomplishmentDAO
    {
		private Connection _connection;

		public IndicatorAccomplishmentDAO()
		{
			_connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			_connection.Dispose();
		}

        public IList<rateDefectsWithinSLA> rateDefectsWithinSLAFbyListTestManufSystemProject(Parameters2 parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorAccomplishment\rateDefectsWithinSLAFbyListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var result = _connection.Executar<rateDefectsWithinSLA>(sql);
            return result;
        }

        public IList<DefectDensity> defectDensitybyListTestManufSystemProject(Parameters2 parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\IndicatorAccomplishment\defectDensityFbyListTestManufSystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var result = _connection.Executar<DefectDensity>(sql);
            return result;
        }
        
    }
}
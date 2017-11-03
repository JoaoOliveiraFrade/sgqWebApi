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
	public class IndOperDevDefectDensityDao {
		private Connection connection;

		public IndOperDevDefectDensityDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

        public IList<DefectDensity> dataFbyProject(string subproject, string delivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indOperDev\defectDensityFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            var result = connection.Executar<DefectDensity>(sql);
            return result;
        }

        public IList<DefectDensity> data(devManufsystemProject parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indOperDev\defectDensityFbydevManufsystemProject.sql"), Encoding.Default);
            sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
            sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
            sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
            var result = connection.Executar<DefectDensity>(sql);
            return result;
        }
    }
}
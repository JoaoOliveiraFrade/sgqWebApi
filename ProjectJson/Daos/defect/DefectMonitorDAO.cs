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
	public class DefectMonitorDao
    {
		private Connection connection;

		public DefectMonitorDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

		public IList<DefectMonitor> FbyQueueStatusTrafficLightProject(DefectMonitorParameter parameter)
		{
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\defect\defectMonitor\fbyQueueStatusTrafficLightProject.sql"), Encoding.Default);

            if(parameter.selectedDefectQueue.Count > 0)
                sql = sql.Replace("@queueFilter", " and encaminhado_para in ('" + string.Join("','", parameter.selectedDefectQueue) + "')");
            else
                sql = sql.Replace("@queueFilter", "");

            if (parameter.selectedDefectStatus.Count > 0)
                sql = sql.Replace("@statusFilter", " and status_atual in ('" + string.Join("','", parameter.selectedDefectStatus) + "')");
            else
                sql = sql.Replace("@statusFilter", "");

            if (parameter.selectedProject.Count > 0)
                sql = sql.Replace("@projectFilter", " and subprojeto + entrega in ('" + string.Join("','", parameter.selectedProject) + "')");
            else
                sql = sql.Replace("@projectFilter", "");

            if (parameter.selectedDefectTrafficLight.Count > 0)
                sql = sql.Replace("@trafficLightFilter", " where trafficLight in ('" + string.Join("','", parameter.selectedDefectTrafficLight) + "')");
            else
                sql = sql.Replace("@trafficLightFilter", "");

            var list = connection.Executar<DefectMonitor>(sql);
			return list;
		}
    }
}

using System.Data.SqlClient;
using Classes;
using ProjectWebApi.Models;
using ProjectWebApi.Models.Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ProjectWebApi.DAOs
{
    public class PulledChainDAO
    {
        private Connection _connection;

        public PulledChainDAO()
        {
            _connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IList<PulledChain> getAll()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\PulledChain\select.sql"));
			var list = _connection.Executar<PulledChain>(sql);
            return list;
        }

        public bool update(PulledChain item)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\PulledChain\update.sql"));
            //sql = sql.Replace("@statusStrategyTestingAndContracting", "statusStrategyTestingAndContracting");
            //sql = sql.Replace("@dtUpdateStrategyTestingAndContracting", "dtUpdateStrategyTestingAndContracting");
            //sql = sql.Replace("@statusTimeline", "statusTimeline");
            //sql = sql.Replace("@dtUpdateTimeline", "dtUpdateTimeline");
            //sql = sql.Replace("@statusTestPlan", "statusTestPlan");
            //sql = sql.Replace("@dtUpdateTestPlan", "dtUpdateTestPlan");
            //sql = sql.Replace("@dtDeliveryTestPlan", "dtDeliveryTestPlan");
            //sql = sql.Replace("@readyTestPlan", "readyTestPlan");
            //sql = sql.Replace("@dtStartTestPlan", "dtStartTestPlan");

            //var connection = new Connection(Bancos.Sgq);

            //connection.Executar(sql)
            try
            {
                var connection = new Connection(Bancos.Sgq);

                bool result = false;
                if (item == null) throw new ArgumentNullException("item");
                if (item.id == 0) throw new ArgumentNullException("id");
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection.connection;
                    command.CommandText = sql;

                    command.Parameters.AddWithValue("id", item.id);
                    command.Parameters.AddWithValue("statusStrategyTestingAndContracting", item.statusStrategyTestingAndContracting);
                    command.Parameters.AddWithValue("dtUpdateStrategyTestingAndContracting", item.dtUpdateStrategyTestingAndContracting);
                    command.Parameters.AddWithValue("dtEndStrategyTestingAndContracting", item.dtEndStrategyTestingAndContracting);

                    command.Parameters.AddWithValue("statusTimeline", item.statusTimeline);
                    command.Parameters.AddWithValue("dtUpdateTimeline", item.dtUpdateTimeline);
                    command.Parameters.AddWithValue("dtEndTimeline", item.dtEndTimeline);

                    command.Parameters.AddWithValue("statusTestPlan", item.statusTestPlan);
                    command.Parameters.AddWithValue("dtUpdateTestPlan", item.dtUpdateTestPlan);
                    command.Parameters.AddWithValue("dtEndTestPlan", item.dtEndTestPlan);

                    command.Parameters.AddWithValue("dtDeliveryTestPlan", item.dtDeliveryTestPlan);
                    command.Parameters.AddWithValue("readyTestPlan", item.readyTestPlan);
                    command.Parameters.AddWithValue("dtStartTestPlan", item.dtStartTestPlan);

                    int i = command.ExecuteNonQuery();
                    result = i > 0;
                }
                connection.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
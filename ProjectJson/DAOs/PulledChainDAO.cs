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
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\PulledChain\select.sql"));
			var list = _connection.Executar<PulledChain>(sql);
            return list;
        }

        public IList<chartCFD> chartCFD()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\PulledChain\chartCFDUpdateHistotic.sql"));
            _connection.Executar(sql);

            sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\PulledChain\chartCFD.sql"));
            var list = _connection.Executar<chartCFD>(sql);
            return list;
        }

        public int update(PulledChain item)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\PulledChain\update.sql"));
            sql = sql.Replace("@statusStrategyTestingAndContracting", item.statusStrategyTestingAndContracting);
            sql = sql.Replace("@dtUpdateStrategyTestingAndContracting", item.dtUpdateStrategyTestingAndContracting);
            sql = sql.Replace("@dtEndStrategyTestingAndContracting", item.dtEndStrategyTestingAndContracting);

            sql = sql.Replace("@statusTimeline", item.statusTimeline);
            sql = sql.Replace("@dtUpdateTimeline", item.dtUpdateTimeline);
            sql = sql.Replace("@dtEndTimeline", item.dtEndTimeline);

            sql = sql.Replace("@statusTestPlan", item.statusTestPlan);
            sql = sql.Replace("@dtUpdateTestPlan", item.dtUpdateTestPlan);
            sql = sql.Replace("@dtEndTestPlan", item.dtEndTestPlan);

            sql = sql.Replace("@dtDeliveryTestPlan", item.dtDeliveryTestPlan);
            sql = sql.Replace("@readyTestPlan", item.readyTestPlan);
            sql = sql.Replace("@dtStartTestPlan", item.dtStartTestPlan);

            sql = sql.Replace("@id", item.id.ToString());

            //        command.Parameters.AddWithValue("statusStrategyTestingAndContracting", item.statusStrategyTestingAndContracting);
            //        command.Parameters.AddWithValue("dtUpdateStrategyTestingAndContracting", item.dtUpdateStrategyTestingAndContracting);
            //        command.Parameters.AddWithValue("dtEndStrategyTestingAndContracting", item.dtEndStrategyTestingAndContracting);

            //        command.Parameters.AddWithValue("statusTimeline", item.statusTimeline);
            //        command.Parameters.AddWithValue("dtUpdateTimeline", item.dtUpdateTimeline);
            //        command.Parameters.AddWithValue("dtEndTimeline", item.dtEndTimeline);

            //        command.Parameters.AddWithValue("statusTestPlan", item.statusTestPlan);
            //        command.Parameters.AddWithValue("dtUpdateTestPlan", item.dtUpdateTestPlan);
            //        command.Parameters.AddWithValue("dtEndTestPlan", item.dtEndTestPlan);

            //        command.Parameters.AddWithValue("dtDeliveryTestPlan", item.dtDeliveryTestPlan);
            //        command.Parameters.AddWithValue("readyTestPlan", item.readyTestPlan);
            //        command.Parameters.AddWithValue("dtStartTestPlan", item.dtStartTestPlan);

            var connection = new Connection(Bancos.Sgq);

            return connection.Executar(sql);
            //try
            //{
            //    var connection = new Connection(Bancos.Sgq);

            //    bool result = false;
            //    if (item == null) throw new ArgumentNullException("item");
            //    if (item.id == 0) throw new ArgumentNullException("id");
            //    using (SqlCommand command = new SqlCommand())
            //    {
            //        command.Connection = connection.connection;
            //        command.CommandText = sql;

            //        command.Parameters.AddWithValue("id", item.id);
            //        command.Parameters.AddWithValue("statusStrategyTestingAndContracting", item.statusStrategyTestingAndContracting);
            //        command.Parameters.AddWithValue("dtUpdateStrategyTestingAndContracting", item.dtUpdateStrategyTestingAndContracting);
            //        command.Parameters.AddWithValue("dtEndStrategyTestingAndContracting", item.dtEndStrategyTestingAndContracting);

            //        command.Parameters.AddWithValue("statusTimeline", item.statusTimeline);
            //        command.Parameters.AddWithValue("dtUpdateTimeline", item.dtUpdateTimeline);
            //        command.Parameters.AddWithValue("dtEndTimeline", item.dtEndTimeline);

            //        command.Parameters.AddWithValue("statusTestPlan", item.statusTestPlan);
            //        command.Parameters.AddWithValue("dtUpdateTestPlan", item.dtUpdateTestPlan);
            //        command.Parameters.AddWithValue("dtEndTestPlan", item.dtEndTestPlan);

            //        command.Parameters.AddWithValue("dtDeliveryTestPlan", item.dtDeliveryTestPlan);
            //        command.Parameters.AddWithValue("readyTestPlan", item.readyTestPlan);
            //        command.Parameters.AddWithValue("dtStartTestPlan", item.dtStartTestPlan);

            //        int i = command.ExecuteNonQuery();
            //        result = i > 0;
            //    }
            //    connection.Dispose();
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }
    }
}
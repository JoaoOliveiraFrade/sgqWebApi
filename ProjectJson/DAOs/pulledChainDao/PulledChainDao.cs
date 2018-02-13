using System.Data.SqlClient;
using Classes;
using ProjectWebApi.Models;
using ProjectWebApi.Models.Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Text;

namespace ProjectWebApi.Daos
{
    public class PulledChainDao
    {
        private Connection connection;

        public PulledChainDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<PulledChain> LoadData()
        {
           
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\pulledChain\loadData.sql"), Encoding.Default);
			var list = connection.Executar<PulledChain>(sql);
            return list;
        }

        public IList<chartCFD> chartCFD()
        {
            string sql1 = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\pulledChain\chatCfdUpdate.sql"), Encoding.Default);
            connection.Executar(sql1);

            string sql2 = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\pulledChain\chartCfdSelect.sql"), Encoding.Default);
            var list = connection.Executar<chartCFD>(sql2);
            return list;
        }

        public int update(PulledChain item)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\pulledChain\update.sql"), Encoding.Default);
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
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
                    command.Parameters.AddWithValue("statusTimeline", item.statusTimeline);
                    command.Parameters.AddWithValue("dtUpdateTimeLine", item.dtUpdateTimeLine);
                    command.Parameters.AddWithValue("statusTestPlan", item.statusTestPlan);
                    command.Parameters.AddWithValue("dtUpdateTestPlan", item.dtUpdateTestPlan);
                    command.Parameters.AddWithValue("dtDeliveryTestPlan", item.dtDeliveryTestPlan);

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
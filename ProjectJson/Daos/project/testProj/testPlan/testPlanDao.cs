using Classes;
using ProjectWebApi.Models;
using ProjectWebApi.Models.Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class testPlanDao
    {
        private Connection connection;

        public testPlanDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<TestPlan> data(string subproject, string delivery)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\testPlan\data.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            return connection.Executar<TestPlan>(sql);
        }
        public IList<Step> step(string subproject, string delivery, string test, string ct)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\testPlan\step.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@test", test);
            sql = sql.Replace("@ct", ct);
            var list = connection.Executar<Step>(sql);
            return list;
        }

    }
}
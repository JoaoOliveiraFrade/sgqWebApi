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
    public class ProjectBitiDao
    {
        private Connection connection;

        public ProjectBitiDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<ProjectBiti> data()
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\biti\data.sql"), Encoding.Default);
            return connection.Executar<ProjectBiti>(sql);
        }
   }
}
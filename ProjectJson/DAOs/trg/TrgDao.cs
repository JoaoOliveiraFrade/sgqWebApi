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
    public class TrgDao
    {
        private Connection connection;

        public TrgDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<IdName> LoadSystems(Release release)
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\systems\loadData.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0,3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length-2));
            return connection.Executar<IdName>(sql);
        }
   }
}
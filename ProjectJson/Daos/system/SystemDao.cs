using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class SystemDao
    {
        private Connection connection;

        public SystemDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<Models.System> data()
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\system\data.sql"), Encoding.Default);
            return connection.Executar<Models.System>(sql);
        }

        public IList<SystemGbyDevManuf> dataGbyDevManuf()
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\system\dataGbyDevManuf.sql"), Encoding.Default);
            return connection.Executar<Models.SystemGbyDevManuf>(sql);
        }

        public IList<SystemGbyTestManuf> dataGbyTestManuf()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\system\dataGbyTestManuf.sql"), Encoding.Default);
            return connection.Executar<SystemGbyTestManuf>(sql);
        }
    }
}

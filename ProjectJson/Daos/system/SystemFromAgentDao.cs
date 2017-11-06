using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class SystemFromAgentDao
    {
        private Connection connection;

        public SystemFromAgentDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<Models.System> data() {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\systemFromAgent\data.sql"), Encoding.Default);
            return connection.Executar<Models.System>(sql);
        }

        public IList<SystemGbyDevManuf> dataGbyDevManuf()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\systemFromAgent\dataGbyDevManuf.sql"), Encoding.Default);
            return connection.Executar<SystemGbyDevManuf>(sql);
        }

        public IList<SystemGbyTestManuf> dataGbyTestManuf()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\systemFromAgent\dataGbyTestManuf.sql"), Encoding.Default);
            return connection.Executar<SystemGbyTestManuf>(sql);
        }
    }
}

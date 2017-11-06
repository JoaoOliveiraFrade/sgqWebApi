using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class DevManufDao
    {
        private Connection connection;

        public DevManufDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<IdName> data()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\devManuf\data.sql"), Encoding.Default);
            var result = connection.Executar<IdName>(sql);
            return result;
        }
        public IList<IdName> dataFromAgent()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\devManuf\dataFromAgent.sql"), Encoding.Default);
            var result = connection.Executar<IdName>(sql);
            return result;
        }
    }
}

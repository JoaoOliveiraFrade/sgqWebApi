using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.DAOs
{
    public class DevManufDAO
    {
        private Connection _connection;

        public DevManufDAO()
        {
            _connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IList<IdName> all()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\devManuf\all.sql"), Encoding.Default);
            var result = _connection.Executar<IdName>(sql);
            return result;
        }
        public IList<IdName> allOfQueue()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\devManuf\allOfQueue.sql"), Encoding.Default);
            var result = _connection.Executar<IdName>(sql);
            return result;
        }
    }
}

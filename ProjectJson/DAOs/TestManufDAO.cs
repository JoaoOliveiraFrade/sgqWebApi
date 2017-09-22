using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.DAOs
{
    public class TestManufDAO
    {
        private Connection _connection;

        public TestManufDAO()
        {
            _connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IList<IdName> all()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\testManuf\all.sql"), Encoding.Default);
            var list = _connection.Executar<IdName>(sql);
            return list;
        }
    }
}

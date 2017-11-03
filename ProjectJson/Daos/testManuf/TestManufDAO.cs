using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class TestManufDao
    {
        private Connection connection;

        public TestManufDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<IdName> all()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\testManuf\all.sql"), Encoding.Default);
            var list = connection.Executar<IdName>(sql);
            return list;
        }
    }
}

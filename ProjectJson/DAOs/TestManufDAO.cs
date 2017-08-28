using Classes;
using ProjectWebApi.Models.TestManuf;
using System.Collections.Generic;
using System.IO;
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

        public IList<TestManuf> getAll()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\TestManuf\TestManufs.sql"));
            var list = _connection.Executar<TestManuf>(sql);
            return list;
        }
    }
}

using Classes;
using ProjectWebApi.Models;
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

        public IList<IdName> all()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\TestManuf\All.sql"));
            var list = _connection.Executar<IdName>(sql);
            return list;
        }
    }
}

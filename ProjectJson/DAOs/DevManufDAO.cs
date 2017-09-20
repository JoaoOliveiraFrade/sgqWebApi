using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
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
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\DevManuf\All.sql"));
            var list = _connection.Executar<IdName>(sql);
            return list;
        }
        public IList<IdName> allOfQueue()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\DevManuf\AllOfQueue.sql"));
            var list = _connection.Executar<IdName>(sql);
            return list;
        }
    }
}

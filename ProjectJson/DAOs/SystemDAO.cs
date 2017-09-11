using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ProjectWebApi.DAOs
{
    public class SystemDAO
    {
        private Connection _connection;

        public SystemDAO()
        {
            _connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IList<IdName> all()
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\System\All.sql"));
			var list = _connection.Executar<IdName>(sql);
            return list;
        }

        public IList<IdName> ofTestManufs(List<string> listTestManufs)
        {
            if (listTestManufs == null)
                return null;

			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\SQLs\System\OfTestManufs.sql"));
			sql = sql.Replace("@listTestManufs", "'" + string.Join("','", listTestManufs) + "'");
			var list = _connection.Executar<IdName>(sql);
            return list;
        }
    }
}

using Classes;
using ProjectWebApi.Models.SystemId;
using ProjectWebApi.Models.SystemByTestManuf;
using ProjectWebApi.Models.TestManuf;
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

        public IList<SystemId> getAll()
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\Systems.sql"));
			var list = _connection.Executar<SystemId>(sql);
            return list;
        }

        public IList<SystemId> getSystemsByTestManuf(List<string> listTestManufs)
        {
            if (listTestManufs == null)
                return null;

			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\SystemsByTestManufs.sql"));
			sql = sql.Replace("@listTestManufs", "'" + string.Join("','", listTestManufs) + "'");
			var list = _connection.Executar<SystemId>(sql);
            return list;
        }
    }
}

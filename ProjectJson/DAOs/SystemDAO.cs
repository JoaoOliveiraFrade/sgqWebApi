using Classes;
using ProjectWebApi.Models.System_;
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

        public IList<System_> getAll()
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\Systems.sql"));
			var list = _connection.Executar<System_>(sql);
            return list;
        }
        public IList<SystemByTestManuf> getSystemsByTestManuf()
        {
            var list = _connection.Executar<SystemByTestManuf>("sp_systems_by_testManuf");
            return list;
        }
        public IList<System_> getSystemsByTestManufs(List<string> listTestManufs)
        {
            if (listTestManufs == null)
                return null;

			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\Systems.sql"));
			sql = sql.Replace("@listTestManufs", "'" + string.Join("','", listTestManufs) + "'");
			var list = _connection.Executar<System_>(sql);
            return list;
        }
    }
}

using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.DAOs
{
    public class SystemDAO
    {
        private Connection connection;

        public SystemDAO()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<IdName> all()
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\all.sql"), Encoding.Default);
            return connection.Executar<IdName>(sql);
        }

        public IList<IdName> fbyTestManufs(List<string> testManufs)
        {
            if (testManufs == null)
                return null;

			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fbyTestManufs.sql"), Encoding.Default);
			sql = sql.Replace("@testManufs", "'" + string.Join("','", testManufs) + "'");
            return connection.Executar<IdName>(sql);
        }

        public IList<IdName> fbyDevManufs(List<string> devManufs)
        {
            if (devManufs == null)
                return null;

            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fbyDevManufs.sql"), Encoding.Default);
            sql = sql.Replace("@devManufs", "'" + string.Join("','", devManufs) + "'");
            return connection.Executar<IdName>(sql);
        }

        public IList<IdName> fromAgentFbyDevManufs(List<string> devManufs) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fromAgentFbyDevManufs.sql"), Encoding.Default);
            return connection.Executar<IdName>(sql);
        }

        public IList<SystemGroupDevManuf> fromAgentGbyDevManufs()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fromAgentGbyDevManufs.sql"), Encoding.Default);
            return connection.Executar<SystemGroupDevManuf>(sql);
        }
    }
}

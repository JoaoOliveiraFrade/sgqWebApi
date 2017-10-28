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
			var list = connection.Executar<IdName>(sql);
            return list;
        }

        public IList<IdName> ofTestManufs(List<string> listTestManufs)
        {
            if (listTestManufs == null)
                return null;

			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\ofTestManufs.sql"), Encoding.Default);
			sql = sql.Replace("@listTestManufs", "'" + string.Join("','", listTestManufs) + "'");
			var list = connection.Executar<IdName>(sql);
            return list;
        }

        public IList<IdName> ofDevManufs(List<string> listDevManufs)
        {
            if (listDevManufs == null)
                return null;

            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\ofDevManufs.sql"), Encoding.Default);
            sql = sql.Replace("@listDevManufs", "'" + string.Join("','", listDevManufs) + "'");
            var list = connection.Executar<IdName>(sql);
            return list;
        }

        public IList<SystemGroupDevManuf> ofQueueGroupDevManufs()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\ofQueueGroupDevManufs.sql"), Encoding.Default);
            var list = connection.Executar<SystemGroupDevManuf>(sql);
            return list;
        }
    }
}

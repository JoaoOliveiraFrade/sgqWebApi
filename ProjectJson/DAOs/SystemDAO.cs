using Classes;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class SystemDao
    {
        private Connection connection;

        public SystemDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }


        #region FromCTAndDefect

            public IList<Models.System> fromCTAndDefec()
            {
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fromCTAndDefec.sql"), Encoding.Default);
                return connection.Executar<Models.System>(sql);
            }

            public IList<SystemGbyDevManuf> fromCTAndDefectGbyDevManuf()
            {
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fromCTAndDefectGbyDevManuf.sql"), Encoding.Default);
                return connection.Executar<Models.SystemGbyDevManuf>(sql);
            }

            public IList<SystemGbyTestManuf> fromCTAndDefectGbyTestManuf()
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fromCTAndDefectGbyTestManuf.sql"), Encoding.Default);
                return connection.Executar<SystemGbyTestManuf>(sql);
            }

        #endregion


        #region FromAgent

            public IList<Models.System> fromAgent() {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fromAgent.sql"), Encoding.Default);
                return connection.Executar<Models.System>(sql);
            }

            public IList<SystemGbyDevManuf> fromAgentGbyDevManuf()
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fromAgentGbyDevManuf.sql"), Encoding.Default);
                return connection.Executar<SystemGbyDevManuf>(sql);
            }

            public IList<SystemGbyTestManuf> fromAgentGbyTestManuf()
            {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\system\fromAgentGbyTestManuf.sql"), Encoding.Default);
                return connection.Executar<SystemGbyTestManuf>(sql);
            }

        #endregion
    }
}

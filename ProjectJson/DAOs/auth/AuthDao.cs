using Classes;
using ProjectWebApi.Models.Profile;
using ProjectWebApi.Models.Project;
using ProjectWebApi.Models.User;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos {
    public class AuthDao {
        private Connection connection;

        public AuthDao() {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose() {
            connection.Dispose();
        }

        public IList<User> LoadUsers() {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\auth\loadUsers.sql"), Encoding.Default);
            return connection.Executar<User>(sql);
        }

        public User LoadUserByCpf(string login, string cpf) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\auth\loadUserByCpf.sql"), Encoding.Default);
            sql = sql.Replace("@login", login);
            sql = sql.Replace("@cpf", cpf);

            var list = connection.Executar<User>(sql);

            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        public IList<Profile> LoadProfilesByUserId(int UserId) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\auth\loadProfilesByUserId.sql"), Encoding.Default);
            sql = sql.Replace("@user", UserId.ToString());

            var list = connection.Executar<Profile>(sql);

            return list;
        }

        public User LoadUserByLogin(string login, string password) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\auth\loadUserByLogin.sql"), Encoding.Default);
            sql = sql.Replace("@login", login);
            sql = sql.Replace("@password", password);

            var list = connection.Executar<User>(sql);

            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

    }
}
using Classes;
using ProjectWebApi.Models.Profile;
using ProjectWebApi.Models.Project;
using ProjectWebApi.Models.User;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class UserDao
    {
        private Connection connection;

        public UserDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<User> getUsers()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\user\users.sql"), Encoding.Default);
            var list = connection.Executar<User>(sql);
            return list;
        }

        public User getUserByCpf(string login, string cpf)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\user\userByCpf.sql"), Encoding.Default);
            sql = sql.Replace("@login", login);
            sql = sql.Replace("@cpf", cpf);

            var list = connection.Executar<User>(sql);

            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        public IList<Profile> getProfilesByUser(int UserId)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\user\profilesByUser.sql"), Encoding.Default);
            sql = sql.Replace("@user", UserId.ToString());

            var list = connection.Executar<Profile>(sql);

            return list;
        }

        public User getUserByPassword(string login, string password)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\user\userByPassword.sql"), Encoding.Default);
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
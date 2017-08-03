using Classes;
using ProjectWebApi.Models.Profile;
using ProjectWebApi.Models.Project;
using ProjectWebApi.Models.User;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ProjectWebApi.DAOs
{
    public class UserDAO
    {
        private Connection _connection;

        public UserDAO()
        {
            _connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IList<User> getUsers()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\User\Users.sql"));
            var list = _connection.Executar<User>(sql);
            return list;
        }

        public User getUserByCpf(string login, string cpf)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\User\UserByCpf.sql"));
            sql = sql.Replace("@login", login);
            sql = sql.Replace("@cpf", cpf);

            var list = _connection.Executar<User>(sql);

            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        public IList<Profile> getProfilesByUser(int UserId)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\User\ProfilesByUser.sql"));
            sql = sql.Replace("@user", UserId.ToString());

            var list = _connection.Executar<Profile>(sql);

            return list;
        }

        public User getUserByPassword(string login, string password)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\DAOs\sqls\User\UserByPassword.sql"));
            sql = sql.Replace("@login", login);
            sql = sql.Replace("@password", password);

            var list = _connection.Executar<User>(sql);

            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

    }
}
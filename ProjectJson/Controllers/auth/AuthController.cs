using ProjectWebApi.Daos;
using ProjectWebApi.Models.Profile;
using ProjectWebApi.Models.User;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplication1.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("auth/loadUsers")]
        public IList<User> LoadUsers()
        {
            var dao = new AuthDao();
            var user = dao.LoadUsers();
            dao.Dispose();
            return user;
        }

        [HttpPost]
        [Route("auth/loadUserByCpf")]
        [ResponseType(typeof(IList<User>))]
        public HttpResponseMessage LoadUserByCpf(HttpRequestMessage request, User user) {
            var dao = new AuthDao();
            var result = dao.LoadUserByCpf(user.login, user.cpf);
            dao.Dispose();
            return request.CreateResponse(result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, result);
        }

        [HttpGet]
        [Route("auth/loadUserByLogin/{login}/{password}")]
        [ResponseType(typeof(IList<Profile>))]
        public User LoadUserByLogin(string login, string password)
        {
            var dao = new AuthDao();
            var user = dao.LoadUserByLogin(login, password);
            dao.Dispose();
            return user;
        }

        [HttpGet]
        [Route("auth/loadProfilesByUserId/{userId}")]
        [ResponseType(typeof(IList<Profile>))]
        public HttpResponseMessage LoadProfilesByUserId(HttpRequestMessage request, int userId) {
            var dao = new AuthDao();
            var result = dao.LoadProfilesByUserId(userId);
            dao.Dispose();
            return request.CreateResponse(result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, result);
        }
    }
}
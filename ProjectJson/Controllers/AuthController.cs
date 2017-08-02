using ProjectWebApi.DAOs;
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
        [Route("auth/users")]
        public IList<User> GetUsers()
        {
            var userDAO = new UserDAO();
            var user = userDAO.getUsers();
            userDAO.Dispose();
            return user;
        }

        [HttpPost]
        [Route("auth/userByCpf")]
        [ResponseType(typeof(IList<User>))]
        public HttpResponseMessage GetUserByCpf(HttpRequestMessage request, User user) {
            var userDAO = new UserDAO();
            var result = userDAO.getUserByCpf(user.login, user.cpf);
            userDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("auth/userByPassword/{login}/{password}")]
        public User GetUserByPassword(string login, string password)
        {
            var userDAO = new UserDAO();
            var user = userDAO.getUserByPassword(login, password);
            userDAO.Dispose();
            return user;
        }
    }
}
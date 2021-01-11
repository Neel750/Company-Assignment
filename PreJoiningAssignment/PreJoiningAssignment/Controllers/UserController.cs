using PMS.BAL.Interface;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PMS.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public IHttpActionResult PostCreateUser([FromBody] UserData model)
        {
            return Ok(_userManager.CreateUser(model));
        }

        public IHttpActionResult PostAuthenticateUser(UserData model)
        {
            return Ok(_userManager.AuthenticateUser(model));
        }
    }
}

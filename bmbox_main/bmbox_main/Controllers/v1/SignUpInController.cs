using bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bmbox_main.Controllers.v1
{
    public class SignUpInController : ApiController
    {
        private AbsRepo<User, int> repo = new UserRepo();

        public bool UserExists(string email)
        {
            return repo.GetAll().Any(u => u.Email == email);

        }
    }
}

using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    class UserRepo : AbsRepo<User>
    {
        public override void Create(User obj)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public override User GetById(int i)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int i)
        {
            throw new NotImplementedException();
        }

        public override void Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}

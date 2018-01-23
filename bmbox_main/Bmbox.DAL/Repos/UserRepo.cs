using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    public class UserRepo : AbsRepo<User>
    {
        public UserRepo() : base()
        {

        }

        public override void Create(User obj)
        {
            //try
            //{
            db.Users.Add(obj);
            db.SaveChanges();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        public override IQueryable<User> GetAll()
        {
            return db.Users;
        }

        public override User GetById(int i)
        {
            User p = db.Users.Find(i);
            if (p == null) return null;
            return p;
        }

        public override void Remove(int i)
        {
            db.Users.Remove(GetById(i));
            //db.Products.Remove(db.Products.Where(id => id.Id == i);
            //db.Products.
            db.SaveChanges();
        }

        public override void Update(User obj)
        {
            //try
            //{
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
    }
}

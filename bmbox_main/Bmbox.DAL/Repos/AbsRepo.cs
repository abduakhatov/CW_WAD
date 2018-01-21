using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    public abstract class AbsRepo<T>
    {
        protected BmboxDBEntities db;

        public AbsRepo()
        {
            db = new BmboxDBEntities();
        }
 
        public abstract IQueryable<T> GetAll();
        public abstract T GetById(int i);
        public abstract void Create(T obj);
        public abstract void Remove(int i);
        public abstract void Update(T obj);
    }
}

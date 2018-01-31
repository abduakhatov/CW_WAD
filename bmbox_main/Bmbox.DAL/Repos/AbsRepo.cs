using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    public abstract class AbsRepo<T, E>
    {
        protected BmboxDataBaseEntities db;

        public AbsRepo()
        {
            db = new BmboxDataBaseEntities();
        }

        public abstract IQueryable<T> GetAll();
        public abstract T GetById(E i);
        public abstract void Create(T obj);
        public abstract void Remove(E i);
        public abstract void Update(T obj);
    }
}

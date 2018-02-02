using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    public class LogRepo : AbsRepo<Log, int>
    {
        public LogRepo() : base()
        {
        }

        public override void Create(Log obj)
        {
            obj.Date = DateTime.Today.Ticks;
            try
            {
                db.Logs.Add(obj);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IQueryable<Log> GetAll()
        {
            return db.Logs;
        }

        public override Log GetById(int i)
        {
            Log p = db.Logs.Find(i);
            return p != null ? p : null ;
        }

        public override void Remove(int i)
        {
            throw new NotImplementedException();
        }

        public override void Update(Log obj)
        {
            throw new NotImplementedException();
        }
    }
}

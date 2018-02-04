using bmbox.DAL.Entities;
using System;
using System.Linq;

namespace Bmbox.DAL.Repos
{
    public class LogRepo : AbsRepo<Log, int>
    {
        private static readonly long DatetimeMinTimeTicks =
      (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

        public LogRepo() : base()
        {
        }

        public override void Create(Log obj)
        {
            obj.Date = ToJavaScriptMilliseconds();
            //UNIX Time = 1300123800000; 


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

        private long ToJavaScriptMilliseconds()
        {
            var date = DateTime.Now;
            var dt =  date.ToUniversalTime().Ticks;
            var dif = dt - DatetimeMinTimeTicks;
            var res = dif / 10000;
            return res;
        }
    }
}

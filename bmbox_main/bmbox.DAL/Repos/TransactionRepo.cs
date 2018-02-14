using bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    public class TransactionRepo : AbsRepo<Transaction, string>
    {
        int id;
        public TransactionRepo() : base()
        {
        }

        public override void Create(Transaction obj)
        {
            try
            {
                var all = GetAll();
                var date = all.Where(u => u.Date == obj.Date);
                var user = date.Where(u => u.UserEmail == obj.UserEmail);

                var statusL = user.Where(t => t.Status == false);
                bool status = true;
                if (statusL.Count() > 0 && statusL != null)
                    status = statusL.First().Status ?? true;

                if (user != null && user.Count() > 0 && status == false) return;

                db.Transactions.Add(obj);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IQueryable<Transaction> GetAll()
        {
            IQueryable<Transaction> res =
                (from tr in db.Transactions
                 join us in db.Users on tr.UserEmail equals us.Email
                 select tr
                );
            return res;
        }

        public override Transaction GetById(string i)
        {

            var p = db.Transactions.Find(id);
            if (p == null) return null;
            return p;
        }

        public override void Remove(string i)
        {
            id = int.Parse(i);
            try
            {
                db.Transactions.Remove(GetById(i));
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void Update(Transaction obj)
        {
            try
            {
                var res = GetAll().Where(t => t.UserEmail == obj.UserEmail && t.Id == obj.Id).First();
                res.Status = obj.Status;
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

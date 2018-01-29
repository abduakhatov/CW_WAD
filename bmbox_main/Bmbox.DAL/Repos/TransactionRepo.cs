using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    public class TransactionRepo : AbsRepo<Transaction>
    {
        public TransactionRepo() : base()
        {
        }

        public override void Create(Transaction obj)
        {
            try
            {
                //obj.UserId = (from u in db.Users
                //              where u.Email == obj.User.Email
                //              select u.Id).First();

                //var all = GetAll();
                //var date = all.Where(u => u.Date == obj.Date);
                //var user = date.Where(u => u.UserId == obj.UserId);


                //if (user != null && user.Count() > 0) return;
                //636527808000000000
                //db.Transactions.Add(obj);
                //db.SaveChanges();
                db.Transactions.Add(new Transaction
                {
                    Date = 636527808000000000,
                    Status = false,
                    UserId = 1
                });
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
                join us in db.Users on tr.UserId equals us.Id
                select tr 
                );
            return res;
        }

        public override Transaction GetById(int i)
        {
            return db.Transactions.Find(i);   
        }

        public override void Remove(int i)
        {
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
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

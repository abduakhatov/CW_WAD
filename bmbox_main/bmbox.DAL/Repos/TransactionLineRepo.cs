using bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    public class TransactionLineRepo : AbsRepo<TransactionLine, int>
    {
        public TransactionLineRepo() : base()
        {

        }

        public override void Create(TransactionLine obj)
        {
            try
            {
                db.TransactionLines.Add(obj);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override IQueryable<TransactionLine> GetAll()
        {
            IQueryable<TransactionLine> res =
                (from tl in db.TransactionLines
                 join p in db.Products on tl.ProductId equals p.Id
                 select tl
                );
            return res;
        }

        public override TransactionLine GetById(int i)
        {
            return db.TransactionLines.Find(i);
        }

        public override void Remove(int i)
        {
            try
            {
                db.TransactionLines.Remove(GetById(i));
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void Update(TransactionLine obj)
        {
            throw new NotImplementedException();
        }
    }
}

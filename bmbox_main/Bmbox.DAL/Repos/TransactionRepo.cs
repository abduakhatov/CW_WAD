using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    class TransactionRepo : AbsRepo<Transaction>
    {
        public override void Create(Transaction obj)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Transaction GetById(int i)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int i)
        {
            throw new NotImplementedException();
        }

        public override void Update(Transaction obj)
        {
            throw new NotImplementedException();
        }
    }
}

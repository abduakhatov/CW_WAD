using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    class TransactionLineRepo : AbsRepo<TransactionLine>
    {
        public override void Create(TransactionLine obj)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<TransactionLine> GetAll()
        {
            throw new NotImplementedException();
        }

        public override TransactionLine GetById(int i)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int i)
        {
            throw new NotImplementedException();
        }

        public override void Update(TransactionLine obj)
        {
            throw new NotImplementedException();
        }
    }
}

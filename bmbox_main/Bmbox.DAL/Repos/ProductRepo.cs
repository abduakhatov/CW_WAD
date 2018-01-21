using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    class ProductRepo : AbsRepo<Product>
    {
        public override void Create(Product obj)
        {
            db.Products.Add(obj);
            db.SaveChanges();
        }

        public override IQueryable<Product> GetAll()
        {
            return db.Products;
        }

        public override Product GetById(int i)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int i)
        {
            throw new NotImplementedException();
        }

        public override void Update(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}

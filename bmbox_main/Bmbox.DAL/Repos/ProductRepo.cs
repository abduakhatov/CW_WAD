using Bmbox.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmbox.DAL.Repos
{
    public class ProductRepo : AbsRepo<Product>
    {
        public ProductRepo() : base()
        {
        }

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
            Product p = db.Products.Find(i);
            if (p == null) return null;
            return p;
        }

        public override void Remove(int i) 
        {
            db.Products.Remove(GetById(i));
            db.SaveChanges();
        }

        public override void Update(Product obj) 
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

using bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bmbox_main.Controllers.v1
{
    public class ProductController : ApiController
    {
        private AbsRepo<Product, int> repo = new ProductRepo();

        [HttpGet]
        public List<ProductViewModel> GetProducts()
        {
            return repo.GetAll().Select(MapToModel).ToList();
        }

        [HttpGet]
        public ProductViewModel Details(int id)
        {
            return MapToModel(repo.GetById(id));
        }

        private ProductViewModel MapToModel(Product p)
        {
            return new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand,
                Type = p.Type,
                Image = p.Image,
                Cost = p.Cost,
                QuantityLeft = p.QuantityLeft
            };
        }
    }
}

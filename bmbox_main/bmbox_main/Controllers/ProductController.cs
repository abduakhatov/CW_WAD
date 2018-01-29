using bmbox_main.Models;
using System.Web.Mvc;
using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using System.Linq;
using bmbox_main.Utils;
using System;
using System.Collections.Generic;
using PagedList;

namespace bmbox_main.Controllers
{
    [Authorize]
    public class ProductController : ParentController
    {
        private AbsRepo<Product> repo = new ProductRepo();
        
        // GET: Product
        public ActionResult Index(string sortOrder, string nameSearch, string typeSearch
            , int? page, string currentNameFilter, string currentTypeFilter)
        {
            var products = repo.GetAll();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TypeSortParam = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";

            if (nameSearch != null)
            {
                page = 1;
            }
            else
            {
                nameSearch = currentNameFilter;
            }

            if (typeSearch != null)
            {
                page = 1;
            }
            else
            {
                typeSearch = currentTypeFilter;
            }

            ViewBag.CurrentNameFilter = nameSearch;
            ViewBag.CurrentTypeFilter = typeSearch;



            var category = (from r in products select r.Type).Distinct().ToList();
            List<string> res = new List<string>();
            res.Add("All");
            res.AddRange(category);

            ViewBag.Categories = res;

            if (!String.IsNullOrEmpty(nameSearch))
            {
                products = products.Where(s => s.Name.Equals(nameSearch));
            }
            if (!String.IsNullOrEmpty(typeSearch) && !typeSearch.Equals("All"))
            {
                products = products.Where(s => s.Type.Equals(typeSearch));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "Type":
                    products = products.OrderBy(p => p.Type);
                    break;
                case "type_desc":
                    products = products.OrderByDescending(p => p.Type);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Cost);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Cost);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 10;
            var pageIndex = (page ?? 1);

            return View(new PagedList<ProductViewModel>(products.Select(MapToModel), pageIndex, pageSize));
            //return View(products.ToPagedList(pageNumber, pageSize));
            //return View(products.Select(MapToModel));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel m)
        {
            try
            {
                var p = MapFromModel(m);
                repo.Create(p);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(MapToModel(repo.GetById(id)));
        }


        [HttpGet]
        public ActionResult Update(int id)
        {
            return View(MapToModel(repo.GetById(id)));
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.Update(MapFromModel(p));
                    return RedirectToAction("Index");
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            repo.Remove(id);
            return RedirectToAction("Index");
        }



        private Product MapFromModel(ProductViewModel m)
        {
            return new Product
            {
                Id = m.Id,
                Name = m.Name,
                Brand = m.Brand,
                Type = m.Type,
                Image = m.Image,
                Cost = m.Cost,
                QuantityLeft = m.QuantityLeft
            };
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
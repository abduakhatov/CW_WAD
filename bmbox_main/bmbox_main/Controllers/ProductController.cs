using bmbox_main.Models;
using System.Web.Mvc;
using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using System.Linq;

namespace bmbox_main.Controllers
{
    public class ProductController : Controller
    {
        private AbsRepo<Product> repo = new ProductRepo();
        // GET: Product
        public ActionResult Index()
        {
            return View(repo.GetAll().Select(MapToModel));
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
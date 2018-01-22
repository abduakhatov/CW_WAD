using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using System.Linq;
using System.Web.Mvc;

namespace bmbox_main.Controllers
{
    public class TransactionLineController : Controller
    {
        AbsRepo<TransactionLine> repo = new TransactionLineRepo();
        private int lastTransactionId;

        // GET: TransactionLine
        public ActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult List(int id)
        {
            return View(repo
                .GetAll()
                .Where(t => t.TransactionId == id)
                .Select(MapToModel));
        }

        // POST: TransactionLine/Create
        [HttpGet]
        public ActionResult Create(int pId, short qnty, int tId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TransactionLine tl = new TransactionLine();
                    tl.ProductId = pId;
                    tl.Quantity = qnty;
                    tl.TransactionId = tId;
                    repo.Create(tl);
                    return RedirectToAction("List/" + tId);
                } 
            }
            catch (System.Exception)
            {
                throw;
            }
            return View();
            
        }

        // POST: TransactionLine/Delete/5
        [HttpGet]
        public ActionResult Delete(int id, int tid)
        {
            try
            {
                repo.Remove(id);
                return RedirectToAction("List/" + tid);
            }
            catch
            {
               return View();
            }
}


        private TransactionLineViewModel MapToModel(TransactionLine p)
        {
            return new TransactionLineViewModel
            {
                Id = p.Id,
                Name = p.Product.Name,
                Brand = p.Product.Brand,
                Type = p.Product.Type,
                Cost = p.Product.Cost,
                Quantity = p.Quantity
            };
        }
    }
}

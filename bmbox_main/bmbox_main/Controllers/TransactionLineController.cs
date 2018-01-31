using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using System.Linq;
using System.Web.Mvc;

namespace bmbox_main.Controllers
{
    [Authorize]
    public class TransactionLineController : Controller
    {
        AbsRepo<TransactionLine, int> repo = new TransactionLineRepo();
        // GET: TransactionLine
        public ActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult List(int id)
        {
            var res = repo
                .GetAll()
                .Where(t => t.TransactionId == id)
                .Select(MapToModel);
            return View(res);
        }


        public void Create(int pId, short qnty, int tId)
        {
            try
            {
                repo.Create(new TransactionLine
                {
                    ProductId = pId,
                    Quantity = qnty,
                    TransactionId = tId,
                });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public ActionResult Details(int id)
        {
            return RedirectToAction("Details", "Product", new { id = id });
        }

        // POST: TransactionLine/Delete/5
        [HttpGet]
        public ActionResult Delete(int transLineId, int TransId)
        {
            try
            {
                repo.Remove(transLineId);
                return RedirectToAction("List/" + TransId);
            }
            catch
            {
               return View();
            }
        }

        public ActionResult Back(string email)
        {
            return RedirectToAction("Index", "Transaction", new { email = email });
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
                Quantity = p.Quantity,
                TransactionId = p.TransactionId,
                total = (decimal)p.Quantity * p.Product.Cost, 
                ProductId = p.ProductId
            };
        }
    }
}

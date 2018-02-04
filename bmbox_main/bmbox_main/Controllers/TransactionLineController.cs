using bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Helpers;
using bmbox_main.Models;
using bmbox_main.Models.Utils;
using System.Linq;
using System.Web.Mvc;

namespace bmbox_main.Controllers
{
    [Authorize]
    public class TransactionLineController : Controller
    {
        AbsRepo<TransactionLine, int> repo = new TransactionLineRepo();
        private Log log = new Log()
        {
            Controller = "TransactionLine"
        };

        // GET: TransactionLine
        public ActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult List(int id)
        {
            log.Action = "Index";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;
            LogHelper.Info(log);
            var res = repo
                .GetAll()
                .Where(t => t.TransactionId == id)
                .Select(MapToModel);
            return View(res);
        }


        public void Create(int pId, short qnty, int tId)
        {
            log.Action = "Index";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;
           
            try
            {
                LogHelper.Info(log);
                repo.Create(new TransactionLine
                {
                    ProductId = pId,
                    Quantity = qnty,
                    TransactionId = tId,
                });
            }
            catch (System.Exception e)
            {
                log.Action = log.Action + " => " + e;
                LogHelper.Error(log);
                throw;
            }
        }

        public ActionResult Details(int id)
        {
            log.Action = "Index";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;
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

using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using bmbox_main.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace bmbox_main.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private AbsRepo<Transaction> repo = new TransactionRepo();
        // GET: Transaction
        public ActionResult Index(string email)
        {
            if (string.IsNullOrEmpty(email)) return View();
            return View(repo.GetAll().Where(u => u.User.Email == email).Select(MapToModel));
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            return View(MapToModel(repo.GetById(id)));
        }
        public ActionResult List(int id)
        {
            return RedirectToAction("List", "TransactionLine", new { Id = id});
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            return View(new TTTVM());
        }

        // POST: Transaction/Create
        [HttpPost]
        public ActionResult Create(TTTVM m)
        {
            int pId = m.ProductId;
            string email = m.Email;
            try
            {
                long today = DateTime.Today.Ticks;

                Transaction t = new Transaction
                {
                    User = new User { Email = email},
                    Date = today
                };
                repo.Create(t);
                TransactionLineController tlc = new TransactionLineController();

                var all = repo.GetAll();
                var date = all.Where(u => u.Date == today);
                var user = date.Where(u => u.User.Email == email);
                var tId = user.First().Id; 


                tlc.Create(pId, 1, tId);

                //636521760000000000   636527808000000000
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Cannot create new record!";
                return View();
            }
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int id)
        {
            repo.Remove(id);
            return RedirectToAction("Index");
        }


        public Transaction MapFromModel(TransactionViewModel m)
        {
            int[] date = m.Date.Split('-').Select(int.Parse).ToArray();
            long dtl = new DateTime(date[2], date[1], date[0]).Ticks;
            return new Transaction
            {
                Id = m.Id,
                Date = dtl,
                User = new User
                {
                    Name = m.Name,
                    LastName = m.LastName,
                    ShippingAdress = m.ShippingAdress
                },
                Status = m.Status.Equals(TransactionStatusEnum.Processed)
            };
        }
        private TransactionViewModel MapToModel(Transaction p)
        {
            return new TransactionViewModel
            {
                Id = p.Id,
                Date = (new DateTime(p.Date).ToString("dd-MM-yyyy")),
                Name = p.User.Name,
                LastName = p.User.LastName,
                ShippingAdress = p.User.ShippingAdress,
                Status = (p.Status == true ? TransactionStatusEnum.Processed : TransactionStatusEnum.Processing)
            };
        }
    }
}



/* DateTime d1 = DateTime.Today;
            long byDate = d1.Ticks;
            label1.Text = d1.ToString();
            label2.Text = byDate.ToString();
            DateTime backDate = new DateTime(byDate);
            label5.Text = backDate.ToString();

            DateTime d2 = DateTime.Now;
            long byNow = DateTime.Now.Ticks;
            label3.Text = d2.ToString();
            label4.Text = byNow.ToString();
            DateTime backNow = new DateTime(byNow);
            label6.Text = backNow.ToString();
 */

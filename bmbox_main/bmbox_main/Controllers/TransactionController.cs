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
        private AbsRepo<Transactions, string> repo = new TransactionRepo();
        // GET: Transaction
        public ActionResult Index(string email)
        {
            if (string.IsNullOrEmpty(email)) return View();
            var res = repo.GetAll().Where(u => u.UserEmail == email);
            return View(res.Select(MapToModel));
        }

        // GET: Transaction/Details/5
        public ActionResult Details(string email)
        {
            return View(MapToModel(repo.GetById(email)));
        }
        public ActionResult List(int id)
        {
            return RedirectToAction("List", "TransactionLine", new { Id = id});
        }

        // POST: Transaction/Create
        public void Create(int pId, string email)
        {
            try
            {
                long today = DateTime.Today.Ticks;
                Transactions t = new Transactions
                {
                    UserEmail = email,
                    Date = today,
                    Status = false
                };
                repo.Create(t);

                var all = repo.GetAll();
                var date = all.Where(u => u.Date == today);
                var user = date.Where(u => u.UserEmail == email && u.Status == false);
                var tId = user.First().Id;

                short quantity = 1;
                new TransactionLineController().Create(pId, quantity, tId);
            }
            catch
            {
                ViewBag.ErrorMessage = "Cannot create new record!";
                return;
            }
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int id, string email)
        {
            repo.Remove(id.ToString());
            return RedirectToAction("Index", new { email = email }); 
        }

        public ActionResult Edit(int id, string email)
        {
           
            repo.Update(new Transactions
            {
                Id = id,
                UserEmail = email,
                Status = true
            });
            return RedirectToAction("Index", new { email = email });
        }


        public Transactions MapFromModel(TransactionViewModel m)
        {
            int[] date = m.Date.Split('-').Select(int.Parse).ToArray();
            long dtl = new DateTime(date[2], date[1], date[0]).Ticks;
            var s = new Transactions
            {
                Id = m.Id,
                UserEmail = m.UserEmail,
                Date = dtl,
                Status = m.Status.Equals(TransactionStatusEnum.Processed),
            };
            return s;
        }
        private TransactionViewModel MapToModel(Transactions p)
        {
            return new TransactionViewModel
            {
                Id = p.Id,
                Date = (new DateTime(p.Date).ToString("dd-MM-yyyy")),
                UserEmail = p.UserEmail,
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

using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using bmbox_main.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmbox_main.Controllers
{
    public class TransactionController : Controller
    {
        private AbsRepo<Transaction> repo = new TransactionRepo();
        // GET: Transaction
        public ActionResult Index()
        {
            return View(repo.GetAll().Select(MapToModel));
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            return View(MapToModel(repo.GetById(id)));
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            return View(new TransactionViewModel());
        }

        // POST: Transaction/Create
        [HttpPost]
        public ActionResult Create(TransactionViewModel t)
        {
            try
            {//636521760000000000
                var p = MapFromModel(t);
                repo.Create(p);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Cannot create new record!";
                return View();
            }
        }

        // GET: Transaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View(MapToModel(repo.GetById(id)));
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(TransactionViewModel t)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repo.Update(MapFromModel(t));
                    return RedirectToAction("Index");
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return View();
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

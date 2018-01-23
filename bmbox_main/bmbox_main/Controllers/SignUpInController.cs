using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace bmbox_main.Controllers
{
    public class SignUpInController : Controller
    {
        private AbsRepo<User> repo = new UserRepo();
        private string passwordSalt;
        // GET: SignInUp
        public ActionResult Index()
        {
            return View(repo.GetAll().Select(MapToModel));
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
                ModelState.AddModelError("ConfirmPassword", "Should match passwords");

            if (ModelState.IsValid)
            {
                repo.Create(MapFromModel(model));
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            repo.Remove(id);
            return RedirectToAction("Index");
        }

        private User MapFromModel(RegistrationViewModel model)
        {
            return new User
            {
                Id = model.Id,
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Password = EncryptPassword(model.Password),
                ShippingAdress = model.ShippingAdress,
            };
        }

        private RegistrationViewModel MapToModel(User p)
        {
            return new RegistrationViewModel
            {
                Id = p.Id,
                Name = p.Name,
                LastName = p.LastName,
                Email = p.Email,
                Password = p.Password,
                ShippingAdress = p.ShippingAdress,
            };
        }

        private string EncryptPassword(string password)
        {
            passwordSalt = Crypto.GenerateSalt();
            var hashedPassword = Crypto.HashPassword(Crypto.SHA256(password));

            return hashedPassword;
        }

        private string DencryptPassword(string password)
        {
            passwordSalt = Crypto.GenerateSalt();
            var hashedPassword = Crypto.HashPassword(Crypto.SHA256(password));

            return hashedPassword;
        }

    }
}
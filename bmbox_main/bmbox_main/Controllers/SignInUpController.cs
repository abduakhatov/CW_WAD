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
    public class SignInUpController : Controller
    {
        private AbsRepo<User> repo = new UserRepo();
        private string passwordSalt;
        // GET: SignInUp
        public ActionResult Index()
        {
            return View();
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


        private User MapFromModel(RegistrationViewModel model)
        {
            return new User
            {

                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Password = EncryptPassword(model.Password),
                ShippingAdress = model.ShippingAdress,
                PasswordSalt = passwordSalt
            };
        }

        private string EncryptPassword(string password)
        {
            passwordSalt = Crypto.GenerateSalt();
            var hashedPassword = Crypto.HashPassword(Crypto.SHA256(password) + passwordSalt);

            return hashedPassword;
        }

        private string DencryptPassword(string password)
        {
            passwordSalt = Crypto.GenerateSalt();
            var hashedPassword = Crypto.HashPassword(Crypto.SHA256(password) + passwordSalt);

            return hashedPassword;
        }

    }
}
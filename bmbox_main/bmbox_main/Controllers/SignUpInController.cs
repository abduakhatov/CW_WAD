using Bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Controllers.Encapsulations;
using bmbox_main.Models;
using bmbox_main.Models.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace bmbox_main.Controllers
{
    //[AllowAnonymous]
    public class SignUpInController : Controller
    {
        private AbsRepo<User> repo = new UserRepo();
        private string passwordSalt;
        // GET: SignInUp
        public ActionResult Index()
        {
            return View();
           // return View(repo.GetAll().Select(MapToModel));
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
            if (!ModelState.IsValid && !ValidateCaptcha())
                return View(model);

            var emailResult = new EmailHandler(model.Email).SendEmail();

            if (emailResult.Equals(Constants.FAIL))
            {
                ViewBag.EmailResult = emailResult;
                return View(model);
            }

            try
            {
                repo.Create(MapFromModel(model));
            }
            catch (Exception)
            {

                return View(model);
            }
            ViewBag.EmailResult = emailResult;
            return RedirectToAction("Index");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string url)
        {
            if (!ModelState.IsValid) return View();

            var user = repo.GetAll().Where(u => u.Email == login.Email).Select(LogInMapToModel).ToList().First();
           
            if (user != null && PasswordIsValid(login.Password, user.Password))
            {
                FormsAuthentication.SetAuthCookie(login.Email, false);
                if (Url.IsLocalUrl(url) && url.Length > 1 && !String.IsNullOrEmpty(url) && Regex.Match(url, @"/([A-Za-z]+)").Success)
                {
                    return Redirect(url);
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            else
            {
                ModelState.AddModelError("", "Authentication Failed");
                return RedirectToAction("Login", "SignUpIn");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
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

        //private RegistrationViewModel MapToModel(User p)
        //{
        //    return new RegistrationViewModel
        //    {
        //        Id = p.Id,
        //        Name = p.Name,
        //        LastName = p.LastName,
        //        Email = p.Email,
        //        Password = p.Password,
        //        ShippingAdress = p.ShippingAdress,
        //    };
        //}
        private LoginViewModel LogInMapToModel(User p)
        {
            return new LoginViewModel
            {
                Email = p.Email,
                Password = p.Password,
            };
        }

        public string EncryptPassword(string password)
        {
            passwordSalt = Crypto.GenerateSalt();
            var hashedPassword = Crypto.HashPassword(Crypto.SHA256(password));

            return hashedPassword;
        }


        public bool PasswordIsValid(string pass, string hashPass)
        {
            passwordSalt = Crypto.GenerateSalt();
            var hashedPassword = Crypto.VerifyHashedPassword(hashPass, Crypto.SHA256(pass));

            return hashedPassword;
        }

        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }

        }

        private bool ValidateCaptcha()
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            string secret = ConfigurationManager.AppSettings["reCAPTCHASecretKey"];
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return false;
                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        ViewBag.message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        ViewBag.message = "The secret parameter is invalid or malformed.";
                        break;
                    case ("missing-input-response"):
                        ViewBag.message = "The response parameter is missing. Please, preceed with reCAPTCHA.";
                        break;
                    case ("invalid-input-response"):
                        ViewBag.message = "The response parameter is invalid or malformed.";
                        break;
                    default:
                        ViewBag.message = "Error occured. Please try again";
                        break;
                }
                return false;
            }
            else
            {
                ViewBag.message = "Valid";
            }
            return true;
        }

    }
}
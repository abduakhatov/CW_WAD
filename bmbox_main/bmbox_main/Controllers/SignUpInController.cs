
using bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Controllers.Encapsulations;
using bmbox_main.Helpers;
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
    [AllowAnonymous]
    public class SignUpInController : ParentController
    {
        private AbsRepo<User, int> repo = new UserRepo();
        private Log log = new Log()
        {
            Controller = "SignUpIn"
        };

        // GET: SignInUp
        public ActionResult Index()
        {
            log.Action = "Index";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;
            LogHelper.Info(log);
            return View();
           // return View(repo.GetAll().Select(MapToModel));
        }

        [HttpGet]
        public ActionResult Register()
        {
            log.Action = "Register";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            var user = User.Identity.Name;
            log.User = user == null || string.IsNullOrEmpty(user)? Constants.LOG_ANONYMOUS : user ;
            
            if (User.Identity.IsAuthenticated)
            {
                log.Action = log.Action + "\nUser is already authenticated";
                LogHelper.Error(log);
                return RedirectToAction("Index", "Product");
            }
            LogHelper.Info(log);
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel model)
        {
            log.Action = "Register";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_POST;
            var logUser = User.Identity.Name;
            log.User = logUser == null || string.IsNullOrEmpty(logUser) ? Constants.LOG_ANONYMOUS : logUser;

           
            if (User.Identity.IsAuthenticated)
            {
                log.Action = log.Action + "\nUser is already authenticated";
                LogHelper.Error(log);
                return RedirectToAction("Index", "Product");
            }
            if (model.Password != model.ConfirmPassword)
            {
                log.Action = log.Action + "\nInvalid Password";
                LogHelper.Error(log);
                ModelState.AddModelError("ConfirmPassword", "Passwords should match");
                return View(model);
            }
            if (!ModelState.IsValid && !ValidateCaptcha())
            {
                log.Action = log.Action + "\nEither model or captcha state is invalid";
                LogHelper.Error(log);
                return View(model);
            }

            var user = repo.GetAll().Any(u => u.Email == model.Email);

            if(user)
            {
                log.Action = log.Action + "\nUser with email already exists";
                LogHelper.Error(log);
                ModelState.AddModelError("Email", "User with email already exists");
                return View(model);
            }

            var emailResult = new EmailHandler(model.Email).SendEmail();

            if (emailResult.Equals(Constants.FAIL))
            {
                log.Action = log.Action + "\nFailed to send email";
                LogHelper.Error(log);
                ViewBag.EmailResult = emailResult;
                return View(model);
            }

            try
            {
                repo.Create(MapFromModel(model));
            }
            catch (Exception)
            {
                log.Action = log.Action + "\nFailed to create";
                LogHelper.Error(log);
                return View(model);
            }
            ViewBag.EmailResult = emailResult;
            log.User = model.Email;
            LogHelper.Info(log);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            log.Action = "Login";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            var logUser = User.Identity.Name;
            log.User = logUser == null || string.IsNullOrEmpty(logUser) ? Constants.LOG_ANONYMOUS : logUser;

            if (User.Identity.IsAuthenticated)
            {
                log.Action = log.Action + "\nUser is already authenticated";
                LogHelper.Error(log);
                return RedirectToAction("Index", "Product");
            }
            LogHelper.Info(log);
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string url)
        {
            if (!ModelState.IsValid) return View();
            log.Action = "Login";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            var logUser = User.Identity.Name;
            log.User = logUser == null || string.IsNullOrEmpty(logUser) ? Constants.LOG_ANONYMOUS : logUser;

            if (User.Identity.IsAuthenticated)
            {
                log.Action = log.Action + "\nUser is already authenticated";
                LogHelper.Error(log);
                return RedirectToAction("Register");
            }

            try
            {
                var user = repo.GetAll().Where(u => u.Email == login.Email).Select(LogInMapToModel).ToList().First();

                if (user != null && PasswordIsValid(login.Password, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Email, false);
                    if (Url.IsLocalUrl(url) && url.Length > 1 && !String.IsNullOrEmpty(url) && Regex.Match(url, @"/([A-Za-z]+)").Success)
                    {
                        //log.User = User.Identity.Name;
                        LogHelper.Info(log);
                        return Redirect(url);
                    }
                    else
                    {
                        LogHelper.Info(log);
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Authentication Failed");
                    return RedirectToAction("Login", "SignUpIn");
                }
            }
            catch (Exception e)
            {
                log.Action = log.Action + "\n" + e;
                LogHelper.Error(log);
                throw;
            }
        }

        public ActionResult LogOut()
        {
            log.Action = "LogOut";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            var logUser = User.Identity.Name;
            log.User = logUser == null || string.IsNullOrEmpty(logUser) ? Constants.LOG_ANONYMOUS : logUser;

            LogHelper.Info(log);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Delete(int id)
        {
            log.Action = "Delete";
            log.IPAddress = Request.UserHostAddress.ToString();
            log.Method = Constants.LOG_METHOD_GET;
            log.User = User.Identity.Name;

            try
            {
                repo.Remove(id);
                LogHelper.Info(log);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                log.Action = log.Action + "\n" + e;
                LogHelper.Error(log);
                throw;
            }
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
            var hashedPassword = Crypto.HashPassword(Crypto.SHA256(password));

            return hashedPassword;
        }


        public bool PasswordIsValid(string pass, string hashPass)
        {
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
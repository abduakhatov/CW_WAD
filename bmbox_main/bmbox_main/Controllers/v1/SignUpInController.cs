using bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bmbox_main.Controllers.v1
{
    public class SignUpInController : ApiController
    {
        private AbsRepo<User, int> repo = new UserRepo();

        public bool UserExists(string email)
        {
            return repo.GetAll().Any(u => u.Email == email);

        }

        //public bool EmailExists(string email)
        //{
        //    var res = new EmailValidationWS.ValidateEmailSoapClient().IsValidEmail(email);

        //    return res;
        //}

        public string ConvertToRub(string r)
        {
            CurrencyConversionWS.CurrencyConvertorSoap objWS = new CurrencyConversionWS.CurrencyConvertorSoapClient();
            double usdToinr = objWS.ConversionRate(CurrencyConversionWS.Currency.USD, CurrencyConversionWS.Currency.INR);
            double totalAmount = usdToinr * Double.Parse(r);
            return totalAmount.ToString();


        }


       
        public string ConvertToRub2()
        {
            CurService.CurrencyConvertorSoapClient client = new CurService.CurrencyConvertorSoapClient();
            String from = "USD";
            String to = "RUB";
            double convValue = client.ConversionRate((CurService.Currency)Enum.Parse(typeof(CurService.Currency), from),
                               (CurService.Currency)Enum.Parse(typeof(CurService.Currency), to));

            return convValue.ToString();

        }


    }
}

using bmbox.DAL.Entities;
using Bmbox.DAL.Repos;
using bmbox_main.Models;
using bmbox_main.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace bmbox_main.Controllers.v1
{
    [Authorize]
    public class LogController : ApiController
    {
        private AbsRepo<Log, int> repo = new LogRepo();

        [HttpGet]
        public List<Log> GetLogs()
        {
            var user = User.Identity.Name.ToString();
            return repo.GetAll().Where(u => u.User == user || u.User == Constants.LOG_ANONYMOUS).OrderByDescending(p => p.Id).ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        public string GetLocation()
        {
            string clientAddress = HttpContext.Current.Request.UserHostAddress;
            GeoService.GeoIPService service = new GeoService.GeoIPService();
            GeoService.GeoIP output = service.GetGeoIP(clientAddress.Trim());
            var r = output.CountryName;
            return string.IsNullOrEmpty(r) ? "Undefined" : r;
        }
    }
}

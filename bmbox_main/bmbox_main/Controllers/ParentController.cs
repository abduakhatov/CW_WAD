using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace bmbox_main.Controllers
{
    public class ParentController : Controller
    {
        private static readonly List<string> _cultures = new List<string> {
            "en-US",
            "en",
            "ru",
            "uz" };

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = RouteData.Values["culture"] as string;
            if (cultureName == null)
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                    Request.UserLanguages[0] : null;

            if (cultureName == null)
                cultureName = _cultures[0];

            cultureName = cultureName.ToLowerInvariant();
            cultureName = _cultures.Any(c => c.ToLowerInvariant().Contains(cultureName)) ? cultureName :
                _cultures[0].ToLowerInvariant();

            if (RouteData.Values["culture"] as string != cultureName)
            {
                RouteData.Values["culture"] = cultureName;
                Response.RedirectToRoute(RouteData.Values);
            }
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }
    }
}
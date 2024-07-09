using Sitecore.Mvc.Presentation;
using Sitecore.Demo.MVC.Web.Models.Feature.About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Demo.MVC.Web.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult AboutExperience()
        {
            // 1.passing item to the view
            var model = new AboutExperience()
            {
                item = RenderingContext.Current.Rendering.Item
            };
            // 2. Read the values, build the model and pass it to view.
            // 3. Read the values using fieldrenderer (supports experience editor)
            return View(model);
        }
    }
}
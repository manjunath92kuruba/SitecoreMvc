using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Demo.MVC.Web.Models.Feature.Home
{
    public class Features
    {
        public List<FeaturesList> FeaturesList { get; set; }
    }

    public class FeaturesList
    {
        public MvcHtmlString Icon { get; set; }
        public MvcHtmlString Title { get; set; }
        public MvcHtmlString Description { get; set; }
    }
}
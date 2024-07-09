using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Demo.MVC.Web.Models.Feature.Home
{
    public class Carousel
    {
        public List<Slide> Slides { get; set; }
    }

    public class Slide
    {
        // we are using fieldrenderers and output of that is MvcHtmlString
        public MvcHtmlString Title { get; set; }
        public MvcHtmlString SubTitle { get; set; }
        public MvcHtmlString Image { get; set; }
        public MvcHtmlString CallToAction { get; set; }
    }
}
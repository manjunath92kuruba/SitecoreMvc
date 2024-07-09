using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Demo.MVC.Web.Models.Feature.Blogs
{
    public class Blogs
    {
        public MvcHtmlString Title { get; set; }

        public MvcHtmlString SubTitle { get; set; }

        public List<BlogItem> BlogsList { get; set; }
    }

    public class BlogItem
    {
        public MvcHtmlString Image { get; set; }
        public MvcHtmlString Title { get; set; }
        public MvcHtmlString ByCopy { get; set; }
        public MvcHtmlString InCopy { get; set; }
        public MvcHtmlString Description { get; set; }
        public MvcHtmlString CallToAction { get; set; }
    }
}
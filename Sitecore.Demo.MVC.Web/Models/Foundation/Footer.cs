using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;

namespace Sitecore.Demo.MVC.Web.Models.Foundation
{
    public class Footer
    {
        public Item Item { get; set; }

        public List<CtaLink> SocialLinks { get; set; }
        public List<CtaLink> ServicesLinks { get; set; }
        public List<CtaLink> UsefulPagesLinks { get; set; }
        public List<CtaLink> FooterLinks { get; set; }
    }

    public class CtaLink
    {
        public MvcHtmlString CallToAction { get; set; }

        public List<CtaLink> SubNavigation { get; set; }
    }
}
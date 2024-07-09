using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Web.Mvc;
//using Glass.Mapper.Sc;
//using Glass.Mapper.Sc.Configuration.Attributes;

namespace Sitecore.Demo.MVC.Web.Models.Foundation
{
    //[SitecoreType]
    public class Header
    {
        public Item Item { get; set; }

        public List<CtaLink> NavLinks { get; set; }

        // Another way of building the navigation using SItecore tree

        public List<Navigation> Navigation { get; set; }
    }

    public class Navigation
    {
        public string NavigationText { get; set; }
        public string NavigationLink { get; set; }
        public string ActiveClass { get; set; }
    }
}
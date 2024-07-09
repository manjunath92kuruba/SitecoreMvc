using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Tours.Models.Shared
{
    [SitecoreType]
    public class Header
    {
        [SitecoreField]
        public virtual Link Logo { get; set; }
        [SitecoreField]
        public virtual string CallUsTitle { get; set; }
        [SitecoreField]
        public virtual string CallUsCopy { get; set; }
        [SitecoreField]
        public virtual string EmailUsTitle { get; set; }
        [SitecoreField]
        public virtual string EmailUsCopy { get; set; }
        [SitecoreField]
        public virtual IEnumerable<NavLinks> NavLinks { get; set; }
    }

    [SitecoreType]
    public class NavLinks
    {
        [SitecoreField]
        public virtual Link CallToAction { get; set; }
    }
}
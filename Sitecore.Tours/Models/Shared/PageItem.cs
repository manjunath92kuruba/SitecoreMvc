using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Tours.Models.Shared
{
    [SitecoreType]
    public class PageItem
    {
        [SitecoreField]
        public virtual string Title { get; set; }
    }
}
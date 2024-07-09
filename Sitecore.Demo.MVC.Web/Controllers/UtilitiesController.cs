//using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Demo.MVC.Web.Models.Foundation;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using Sitecore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Demo.MVC.Web.Extensions;

namespace Sitecore.Demo.MVC.Web.Controllers
{
    public class UtilitiesController : Controller
    {
        // GET: Utilities
        public ActionResult Header()
        {

            //List<NavLink> navLink = new List<NavLink>();
            var datasource = RenderingContext.Current?.Rendering.Item;
            var model = new Header()
            {
                Item = datasource
            };
            MultilistField navLinksField = datasource.Fields["NavLinks"];

            // commented below code and created a common method to get multilist values

            //if (navLinksField.Count > 0)
            //{
            //    var navLinkItems = navLinksField.GetItems();
            //    foreach (var navLinkItem in navLinkItems)
            //    {
            //        // one way of getting the value
            //        //var callToAction = new MvcHtmlString(navLinkItem.Fields["CallToAction"]?.Value);

            //        // ANother way of getting the value and it can be edited in experience editor
            //        var callToAction = new MvcHtmlString(FieldRenderer.Render
            //            (navLinkItem, "CallToAction", "class = nav-item nav-link"));

            //        navLink.Add(new NavLink
            //        {
            //            CallToAction = callToAction
            //        });

            //    }
            //    model.NavLinks = navLink;
            //}
            string className = "class = nav-item nav-link";

            // Below code is for optimisation to read the multilist field with child items

            //string childClassName = "class=dropdown - item";
            //if (navLinksField.Count>0)
            //{
            //    List<CtaLink> parentNavigations = new List<CtaLink>();
            //    List<CtaLink> childNavigations = new List<CtaLink>();
            //    var navigationItems = navLinksField.GetItems();
            //    foreach(var navigationItem in navigationItems)
            //    {
            //        // Getting field values of first level of items
            //        var parentNavigation = getLinksData(navigationItem, className);
            //        if(navigationItem.HasChildren)
            //        {
            //            var subNavigationItems = navigationItem.Children;
            //            foreach (Item subNavigationItem in subNavigationItems)
            //            {
            //                // getting field values of second level of items
            //                var childNavigation = getLinksData(subNavigationItem, childClassName);
            //                childNavigations.Add(childNavigation);
            //            }
            //            parentNavigation.SubNavigation = childNavigations;
            //        }
            //        parentNavigations.Add(parentNavigation);
            //    }
            //    model.NavLinks = parentNavigations;
            //}


            model.NavLinks = GetLinkMultiListValues(navLinksField, className);

            return View(model);
        }

        public CtaLink getLinksData(Item item, string className)
        {
            var callToAction = new MvcHtmlString(FieldRenderer.Render(item, "CallToAction", className));
            return new CtaLink
            {
                CallToAction = callToAction
            };
        }


        #region method to get header data from sitecore tree and not from data source
        public ActionResult GetNavigationFromTree()
        {
            var model = new Header();
            // Below code is to read the start item and the build navigation from sitecore tree
            List<Navigation> navigation = new List<Navigation>();
            Item homeItem = Sitecore.Context.Site.HomeItem();
            navigation.Add(BuildNavigation(homeItem));
            if(homeItem.HasChildren)
            {
                foreach(Item childItem in homeItem.Children)
                {
                    navigation.Add(BuildNavigation(childItem));
                }
            }
            model.Navigation = navigation;
            return View(model);
        }

        public Navigation BuildNavigation(Item navItem)
        {
            return new Navigation
            {
                NavigationText = navItem.Fields["Title"]?.Value,
                NavigationLink = navItem.Url(),
                ActiveClass = PageContext.Current.Item.ID == navItem.ID ? "active" : string.Empty
            };
        }

        #endregion

        // This is to read the multilist field values with one level of items

        public List<CtaLink> GetLinkMultiListValues(MultilistField multiListField, string className)
        {
            List<CtaLink> ctalinks = new List<CtaLink>();
            if(multiListField.Count>0)
            {
                var multiListItems = multiListField.GetItems();
                foreach(var multiListItem in multiListItems)
                {
                    var callToAction = new MvcHtmlString(FieldRenderer.Render(multiListItem, "CallToAction", className));
                    ctalinks.Add(new CtaLink
                    {
                        CallToAction = callToAction
                    });
                }
            }
            return ctalinks;
        }

        public ActionResult Footer()
        {
            string socialLinksClassName = "class=fab fa-twitter";
            string noClassName = "";
            var dataSource = RenderingContext.Current.Rendering.Item;
            var model = new Footer()
            {
                Item = dataSource
            };
            MultilistField socialLinks = dataSource.Fields["SocialLinks"];
            MultilistField serviceLinks = dataSource.Fields["ServicesLinks"];
            MultilistField usefulPagesLinks = dataSource.Fields["UsefulPagesLinks"];
            MultilistField footerLinks = dataSource.Fields["FooterLinks"];
            model.SocialLinks = GetLinkMultiListValues(socialLinks, socialLinksClassName);
            model.ServicesLinks = GetLinkMultiListValues(serviceLinks, noClassName);
            model.UsefulPagesLinks = GetLinkMultiListValues(usefulPagesLinks, noClassName);
            model.FooterLinks = GetLinkMultiListValues(footerLinks, noClassName);
            return View(model);
        }
    }
}
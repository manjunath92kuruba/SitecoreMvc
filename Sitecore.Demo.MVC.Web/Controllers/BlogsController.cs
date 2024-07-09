using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Demo.MVC.Web.Models.Feature.Blogs;
using Sitecore.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;
using Sitecore.Demo.MVC.Web.Extensions;

namespace Sitecore.Demo.MVC.Web.Controllers
{
    public class BlogsController : Controller
    {
        // GET: Blogs
        public ActionResult BlogsLanding()
        {
            string blogTemplateId = "{9270A7F3-2390-44B1-BB70-FEB0B4EA628E}";
            var templateId = new ID(blogTemplateId);
            // getting the ucket item
            var dataSource = Sitecore.Context.Item;
            string bucketPath = dataSource.Paths.FullPath;
            var model = new Blogs();
            List<BlogItem> blogItems = new List<BlogItem>();
            if (dataSource!=null)
            {
                model.Title = new MvcHtmlString(FieldRenderer.Render(dataSource, "Title"));
                model.SubTitle = new MvcHtmlString(FieldRenderer.Render(dataSource, "SubTitle"));
                // Get all the items under bucket item and respective field values within those item
                // Creating an index to search in, this index needs to be like below and it should be in Sitecore->control panel->indexing manager
                var index = ContentSearchManager.GetIndex("sitecore_web_index");
                // Setup a search context
                using(var context = index.CreateSearchContext())
                {
                    // Build a query to get items within the bucket with a specific template
                    var query = context.GetQueryable<SearchResultItem>().Where(item => item.Path.StartsWith(bucketPath) && item.TemplateId == templateId);

                    var results = query.ToList();

                    List<Item> bucketItems = results.Select(result => Sitecore.Context.Database.GetItem(result.GetItem().ID)).ToList();
                    foreach (Item bucketItem in bucketItems)
                    {
                        blogItems.Add(new BlogItem
                        {
                            Image = new MvcHtmlString(FieldRenderer.Render(bucketItem, "Image")),
                            Title = new MvcHtmlString(FieldRenderer.Render(bucketItem, "Title")),
                            ByCopy = new MvcHtmlString(FieldRenderer.Render(bucketItem, "ByCopy")),
                            InCopy = new MvcHtmlString(FieldRenderer.Render(bucketItem, "InCopy")),
                            Description = new MvcHtmlString(FieldRenderer.Render(bucketItem, "Description")),
                            CallToAction = new MvcHtmlString(bucketItem.Url())
                        });
                        model.BlogsList = blogItems;
                    }
                }
            }
            //return Json(model, "~/Views/Blogs/BlogsLanding.cshtml", JsonRequestBehavior.AllowGet);
            return View(model);
        }

        public ActionResult BlogsDetail()
        {
            //BlogItem model = new BlogItem();
            //Item pageItem = Sitecore.Context.Item;
            //if(pageItem !=null)
            //{
            //    model.
            //}
            return View();
        }
    }
}
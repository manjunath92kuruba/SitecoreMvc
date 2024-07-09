using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Demo.MVC.Web.Models.Feature.Home; 
using System.Web.Mvc;
using Sitecore.Web.UI.WebControls;
using Sitecore.Data.Items;

namespace Sitecore.Demo.MVC.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Carousel()
        {
            Carousel model = new Carousel();
            List<Slide> slides = new List<Slide>();
            var dataSource = RenderingContext.Current.Rendering.Item;
            MultilistField slidesField = dataSource.Fields["Slides"];

            // Rendering paramters concept
            //var slideCount = RenderingContext.Current.Rendering.Parameters["SlideCount"];
            //int.TryParse(slideCount, out int result);
            //int slideCount = result == 0 ? 2 : 3;

            if(slidesField.Count>0)
            {
                var slideItems = slidesField.GetItems();
                foreach(var slideItem in slideItems)
                {
                    //Two ways to get the values written below
                    // 1.Title - using normal fields but not editable in EE
                    //var titleField = slideItem.Fields["Title"];
                    //var title = new MvcHtmlString(slideItem.Fields["Title"].Value);
                    //var subTitle = new MvcHtmlString(slideItem.Fields["SubTitle"].Value);
                    //var image = new MvcHtmlString(slideItem.Fields["Image"].Value);
                    //var callToAction = new MvcHtmlString(slideItem.Fields["CallToAction"].Value);

                    // 2.SubTitle - using FieldRenderer used for editing in EE
                    var title = new MvcHtmlString(FieldRenderer.Render(slideItem, "Title"));
                    var subTitle = new MvcHtmlString(FieldRenderer.Render(slideItem, "SubTitle"));
                    var image = new MvcHtmlString(FieldRenderer.Render(slideItem, "Image"));
                    var callToAction = new MvcHtmlString(FieldRenderer.Render(slideItem, "CallToAction", "class = btn animated fadeInUp"));
                    slides.Add(new Slide
                    {
                        Title = title,
                        SubTitle = subTitle,
                        Image = image,
                        CallToAction = callToAction
                    });
                }
                model.Slides = slides;
            }
            return View(model);
        }

        #region IconTitleDescription
        // Home Features component
        public ActionResult Features()
        {
            var model = new Features();
            var parentItem = RenderingContext.Current.Rendering.Item;
            if(parentItem.HasChildren)
            {
                model.FeaturesList = GetChildItemFields(parentItem);
            }
            return View(model);
        }

        public List<FeaturesList> GetChildItemFields(Item item)
        {
            List<FeaturesList> featuresList = new List<FeaturesList>();
            foreach (Item childItem in item.Children)
            {
                featuresList.Add(new FeaturesList
                {
                    Icon = new MvcHtmlString(FieldRenderer.Render(childItem, "Icon")),
                    Title = new MvcHtmlString(FieldRenderer.Render(childItem, "Title")),
                    Description = new MvcHtmlString(FieldRenderer.Render(childItem, "Description"))
                });
            }
            return featuresList;
        }

        #endregion

        #region Home Facts

        public ActionResult Facts()
        {
            var dataSource = RenderingContext.Current.Rendering.Item;
            MultilistField factsOne = dataSource.Fields["FactsOne"];
            MultilistField factsTwo = dataSource.Fields["FactsTwo"];
            var model = new Facts();
            model.FactsOne = GetFeaturesLists(factsOne);
            model.FactsTwo = GetFeaturesLists(factsTwo);
            return View(model);
        }

        public List<FeaturesList> GetFeaturesLists(MultilistField multilistField)
        {
            List<FeaturesList> featureList = new List<FeaturesList>();
            if (multilistField.Count>0)
            {
                var listItems = multilistField.GetItems();
                foreach(var listItem in listItems)
                {
                    featureList.Add(new FeaturesList
                    {
                        Icon = new MvcHtmlString(FieldRenderer.Render(listItem, "Icon")),
                        Title = new MvcHtmlString(FieldRenderer.Render(listItem, "Title")),
                        Description = new MvcHtmlString(FieldRenderer.Render(listItem, "Description"))
                    });
                }
            }
            return featureList;
        }

        #endregion
    }
}
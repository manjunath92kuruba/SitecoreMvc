using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Buckets.Extensions;
using Sitecore.Buckets.Managers;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.IO;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Sites;

namespace Sitecore.Demo.MVC.Web.Extensions
{
    public static class SiteExtensions
    {
        // To get the start or home item for a site
        public static Item HomeItem(this SiteContext siteContext)
        {
            //var startPath = Sitecore.Context.Site.StartPath;
            var startPath = siteContext.StartPath;
            var homeItem = Sitecore.Context.Database.GetItem(startPath);
            return homeItem;
        }
    }

    public static class ItemExtensions
    {
        public static string Url(this Item item, ItemUrlBuilderOptions options=null)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (options != null)
                return LinkManager.GetItemUrl(item, options);
            return LinkManager.GetItemUrl(item);
        }
    }

    // Method to ovverride the ItemUrlBuilder of Sitecore.kernal with our cutom code. Below method will be executed instead of Sitecore method by using config patch
    public class CustomBlogLink: ItemUrlBuilder
    {
        public CustomBlogLink(DefaultItemUrlBuilderOptions option):base(option)
        {

        }
        public override string Build(Item item, ItemUrlBuilderOptions options)
        {

            if (BucketManager.IsItemContainedWithinBucket(item))
            {
                var bucketItem = item.GetParentBucketItemOrParent();
                if (bucketItem != null && bucketItem.IsABucket())
                {
                    var bucketUrl = base.Build(bucketItem, options);
                    return FileUtil.MakePath(bucketUrl, item.Name);
                }
            }
            return base.Build(item, options);
        }
    }

    public class CustomBlogLinkResolver: HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            string blogTemplateId = "{9270A7F3-2390-44B1-BB70-FEB0B4EA628E}";
            var templateId = new ID(blogTemplateId);
            // If sitecore is not able to resolve the incoming url in our case blogs/blog-one
            if (Context.Item == null)
            {
                var requestUrl = args.Url.ItemPath;
                var index = requestUrl.LastIndexOf('/');
                if(index>0)
                {
                    var bucketPath = requestUrl.Substring(0, index);
                    var bucketItem = args.GetItem(bucketPath);
                    if(bucketItem!=null && BucketManager.IsBucket(bucketItem))
                    {
                        var itemName = requestUrl.Substring(index + 1);
                        using (var searchContext = ContentSearchManager.GetIndex("sitecore_web_index").CreateSearchContext())
                        {
                            var result = searchContext.GetQueryable<SearchResultItem>().Where(x => x.Name == itemName && x.TemplateId == templateId).FirstOrDefault();
                            if (result != null)
                                Context.Item = result.GetItem();
                        }
                    }
                }
            }
        }
    }
}
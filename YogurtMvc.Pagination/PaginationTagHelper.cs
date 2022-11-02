using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace YogurtMvc.Pagination
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public IPagedList PageModel { get; set; }
        public string PageAction { get; set; }

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var ulBuilder = new TagBuilder("ul");
            ulBuilder.AddCssClass("pagination");
            string anchorInnerHtml = "";

            ulBuilder.

            for (var i = 1; i <= PageModel.PageCount; i++)
            {
                var liBuilder = new TagBuilder("li");
                liBuilder.AddCssClass("page-item");

                var aBuilder = new TagBuilder("a");
                aBuilder.AddCssClass("page-link");
                anchorInnerHtml = AnchorInnerHtml(i, PageModel);
                aBuilder.InnerHtml.AppendHtml(anchorInnerHtml);

                if (anchorInnerHtml == "..")
                {
                    liBuilder.AddCssClass("disabled");
                    aBuilder.Attributes["href"] = "#";
                }
                else
                    aBuilder.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });

                liBuilder.InnerHtml.AppendHtml(aBuilder);

                if (anchorInnerHtml != "")
                    ulBuilder.InnerHtml.AppendHtml(liBuilder);
            }
            output.Content.AppendHtml(ulBuilder);
        }

        public static string AnchorInnerHtml(int i, IPagedList pagingInfo)
        {
            string anchorInnerHtml = "";
            if (pagingInfo.PageCount <= 10)
                anchorInnerHtml = i.ToString();
            else
            {
                if (pagingInfo.CurrentPage <= 5)
                {
                    if ((i <= 8) || (i == pagingInfo.PageCount))
                        anchorInnerHtml = i.ToString();
                    else if (i == pagingInfo.PageCount - 1)
                        anchorInnerHtml = "..";
                }
                else if ((pagingInfo.CurrentPage > 5) && (pagingInfo.PageCount - pagingInfo.CurrentPage >= 5))
                {
                    if ((i == 1) || (i == pagingInfo.PageCount) || ((pagingInfo.CurrentPage - i >= -3) && (pagingInfo.CurrentPage - i <= 3)))
                        anchorInnerHtml = i.ToString();
                    else if ((i == pagingInfo.CurrentPage - 4) || (i == pagingInfo.CurrentPage + 4))
                        anchorInnerHtml = "..";
                }
                else if (pagingInfo.PageCount - pagingInfo.CurrentPage < 5)
                {
                    if ((i == 1) || (pagingInfo.PageCount - i <= 7))
                        anchorInnerHtml = i.ToString();
                    else if (pagingInfo.PageCount - i == 8)
                        anchorInnerHtml = "..";
                }
            }
            return anchorInnerHtml;
        }
    }
}

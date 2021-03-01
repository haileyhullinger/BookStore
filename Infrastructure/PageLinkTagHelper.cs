using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//buidling the navigation for each page (page numbers at bottom)
namespace BookStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        //helper factory holding information
        private IUrlHelperFactory urlHelperFactory;

        //constructor that collect the helper factory holding information
        public PageLinkTagHelper(IUrlHelperFactory hp)
        {
            urlHelperFactory = hp;
        }

        //attributes attached to the ViewContext property
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        //creating an instance of the Model that we created for page info
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }

        //setting up a key pair to keep track of the classiciation that it is sorting by to keep track of in the tag helper. Have a dictionary of items stored as entries every time a user enters page-url-
        [HtmlAttributeName(DictionaryAttributePrefix ="page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        //information for styling the page numbers
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }



        //override a method from the TagHelper (replace a set method with our own method) 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            TagBuilder result = new TagBuilder("div");

            //building html on the fly, looping throug the pages
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                //link to a page
                TagBuilder tag = new TagBuilder("a");

                //set what page we are on (to keep track of category)
                PageUrlValues["page"] = i;

                //reference to the next page
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                //if statement to add CSS classes to page nagivation links
                if (PageClassesEnabled)
                {
                    //add CSS classes
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                //append to html, i represents the page number that we are in the loop
                tag.InnerHtml.Append(i.ToString());

                //pass the tag we created to append to the page
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}


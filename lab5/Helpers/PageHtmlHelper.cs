using System;
using System.Text;
using lab5.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace lab5.Helpers
{
    //Работает в /Diseases/Index
    public static class PageHtmlHelper
    {
        public static HtmlString CreateLinksToPages(this IHtmlHelper html, PageViewModel pageView)
        {
            TagBuilder div = new TagBuilder("div");
            if (pageView.HasPreviousPage) {
                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("href", "/Disease?page=" + (pageView.PageNumber - 1));
                a.Attributes.Add("class ", "btn btn-default btn");
                a.InnerHtml.Append("Назад");
                div.InnerHtml.AppendHtml(a);
            }
            if (pageView.HasNextPage) {
                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("href", "/Disease?page=" + (pageView.PageNumber + 1));
                a.Attributes.Add("class ", "btn btn-default btn");
                a.InnerHtml.Append("Вперёд");
                div.InnerHtml.AppendHtml(a);
            }

            var writer = new System.IO.StringWriter();
            div.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
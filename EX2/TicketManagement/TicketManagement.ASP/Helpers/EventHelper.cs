using System;
using System.Web.Mvc;
using DataPresenter.Entity;

namespace TicketManagement.ASP.Helpers
{
    public static class EventHelper
    {
        public static MvcHtmlString EventItem(this HtmlHelper helper, Event it)
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder sp = new TagBuilder("span");

            if (it.EventDate >= DateTime.Now)
            {
                sp.SetInnerText(it.Name);
                sp.SetInnerText(it.Description);
                sp.SetInnerText(it.EventDate.ToLongDateString());
                li.InnerHtml = sp.ToString();
            }
            return new MvcHtmlString(li.ToString());

        }
    }
}

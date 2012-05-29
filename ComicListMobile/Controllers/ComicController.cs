using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client.Embedded;
using Raven.Client.Document;
using ComicListMobile.Models;
using Raven.Client;
using ComicListMobile.RavenDB.Indexes;
using System.Globalization;

namespace ComicListMobile.Controllers
{
    public class ComicController : Controller
    {
        protected IDocumentStore DocumentStore { get; private set; }
        protected IDocumentSession PerRequestSession { get; private set; }


        public ComicController(IDocumentStore documentStore, IDocumentSession session)
        {
            DocumentStore = documentStore;
            PerRequestSession = session;
        }

        //
        // GET: /Comic/

        public ActionResult Publisher(int? year, int? week)
        {
            int wantedYear = year.HasValue ? year.Value : DateTime.Today.Year;
            int wantedWeek = week.HasValue ? week.Value : DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(DateTime.Now,CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var comicsForWeek = PerRequestSession.Query<Comic, ComicDefaultIndex>().Where(c => c.ReleaseWeek == wantedWeek && c.ReleaseYear == wantedYear).ToList();

            return View("ComicsByPublisher", new ComicModel() { ComicsForWeek = comicsForWeek });
        }

    }
}

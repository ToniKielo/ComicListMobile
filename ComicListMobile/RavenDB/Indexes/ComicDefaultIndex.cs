using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComicListMobile.Models;
using Raven.Client.Indexes;

namespace ComicListMobile.RavenDB.Indexes
{
    public class ComicDefaultIndex : AbstractIndexCreationTask<Comic>
    {
        public ComicDefaultIndex()
        {
            Map = comics => from c in comics
                            select new
                            {
                                ReleaseYear = c.ReleaseYear,
                                ReleaseWeek = c.ReleaseWeek
                            };
        }
    }
}
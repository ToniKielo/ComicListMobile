using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComicListMobile.Models
{
    public class Comic
    {
        public string DiamondIdentifier { get; set; }
        public string Name { get; set; }
        public string Volume { get; set; }
        public string IssueNumber { get; set; }
        public int ReleaseYear { get; set; }
        public int ReleaseWeek { get; set; }

    }
}
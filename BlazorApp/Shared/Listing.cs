using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class Listing
    {
        public string Name { get; set; }
        public string[] Description { get; set; }
        public string RepositoryLink { get; set; }
        public string GHPagesLink { get; set; }
    }
}

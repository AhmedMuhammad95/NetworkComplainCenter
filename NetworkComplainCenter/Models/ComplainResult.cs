using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkComplainCenter.Models
{
    public class ComplainResult
    {
        public List<Complain> Complains { get; set; }

        public int CurrentPage { get; set; }

        public int TotalComplains { get; set; }

        public int PageSize { get; set; }
    }
}
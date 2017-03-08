using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkComplainCenter.Models
{
    public class UserResult
    {
        public List<User> Users { get; set; }

        public int CurrentPage { get; set; }

        public int TotalUsers { get; set; }

        public int PageSize { get; set; }
    }
}
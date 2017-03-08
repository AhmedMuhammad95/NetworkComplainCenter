using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkComplainCenter.Models
{
    public class ComputerResult
    {
        public List<Computer> Computers { get; set; }

        public int CurrentPage { get; set; }

        public int TotalComputers { get; set; }

        public int PageSize { get; set; }
    }
}
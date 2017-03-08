using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkComplainCenter.Models
{
    public class LocationResult
    {
        public List<Location> Locations { get; set; }

        public int CurrentPage { get; set; }

        public int TotalLocations { get; set; }

        public int PageSize { get; set; }
    }
}
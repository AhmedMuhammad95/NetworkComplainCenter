using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetworkComplainCenter.Models;

namespace NetworkComplainCenter.Controllers
{
    public class LocationController : Controller
    {
        ComplainCenterEntities db = new ComplainCenterEntities();
        public ActionResult Index(int p = 0, int pageSize = 10, string keyword = "", string sortBy = "")
        {
            List<Location> locations = null;
            var _total = 0;

            if (String.IsNullOrEmpty(keyword))
            {
                switch (sortBy)
                {
                    case "Name":
                        locations = db.Locations.OrderBy(x => x.Name).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        locations = db.Locations.OrderBy(x => x.Id).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                }
                _total = db.Locations.Count();
            }
            else
            {
                keyword = keyword.ToLower();

                switch (sortBy)
                {
                    case "Name":
                        locations = db.Locations.Where(c => c.Name.ToLower().Contains(keyword))
                    .OrderBy(x => x.Name).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        locations = db.Locations.Where(c => c.Name.ToLower().Contains(keyword))
                   .OrderBy(x => x.Id).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                }

                _total = db.Locations.Count(c => c.Name.ToLower().Contains(keyword));
            }

            LocationResult result = new LocationResult();
            result.Locations = locations;
            result.CurrentPage = p;
            result.PageSize = pageSize;
            result.TotalLocations = _total;

            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string name)
        {
            Location l = new Location();

            l.Name = name;

            db.Locations.Add(l);
            db.SaveChanges();

            TempData["message"] = "User added.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            var obj = db.Locations.Find(Id);
            db.Locations.Remove(obj);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

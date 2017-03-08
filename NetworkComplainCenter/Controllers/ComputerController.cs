using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetworkComplainCenter.Models;

namespace NetworkComplainCenter.Controllers
{
    public class ComputerController : Controller
    {
        ComplainCenterEntities db = new ComplainCenterEntities();
        public ActionResult Index(int p = 0, int pageSize = 10, string keyword = "", string sortBy = "")
        {
            List<Computer> computers = null;
            var _total = 0;

            if (String.IsNullOrEmpty(keyword))
            {
                switch (sortBy)
                {
                    case "Name":
                        computers = db.Computers.OrderBy(x => x.Name).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "LocationId":
                        computers = db.Computers.OrderBy(x => x.LocationId).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "ComputerStatusId":
                        computers = db.Computers.OrderBy(x => x.ComputerStatusId).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        computers = db.Computers.OrderBy(x => x.Id).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                }
                _total = db.Computers.Count();
            }
            else
            {
                keyword = keyword.ToLower();

                switch (sortBy)
                {
                    case "Name":
                        computers = db.Computers.Where(c => c.Name.ToLower().Contains(keyword))
                    .OrderBy(x => x.Name).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "LocationId":
                        computers = db.Computers.Where(c => c.Name.ToLower().Contains(keyword))
                     .OrderBy(x => x.LocationId).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "ComputerStatusId":
                        computers = db.Computers.Where(c => c.Name.ToLower().Contains(keyword))
                    .OrderBy(x => x.ComputerStatusId).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        computers = db.Computers.Where(c => c.Name.ToLower().Contains(keyword))
                   .OrderBy(x => x.Id).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                }

                _total = db.Computers.Count(c => c.Name.ToLower().Contains(keyword));
            }

            ComputerResult result = new ComputerResult();
            result.Computers = computers;
            result.CurrentPage = p;
            result.PageSize = pageSize;
            result.TotalComputers = _total;

            return View(result);
        }
        public ActionResult Create()
        {
            var locations = db.Locations.ToList();

            ViewBag.Locations = locations;
            return View();
        }
        [HttpPost]
        public ActionResult Create(int location_id, string name)
        {
            Computer c = new Computer();
            
            c.ComputerStatusId = 1;
            c.Name = name;
            c.LocationId = location_id;

            db.Computers.Add(c);
            db.SaveChanges();

            TempData["message"] = "Computer added.";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            var obj = db.Computers.Find(Id);
            db.Computers.Remove(obj);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
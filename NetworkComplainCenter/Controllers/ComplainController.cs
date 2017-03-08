using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetworkComplainCenter.Models;

namespace NetworkComplainCenter.Controllers
{
    public class ComplainController : Controller
    {
        ComplainCenterEntities db = new ComplainCenterEntities();
        public ActionResult Index(int p = 0, int pageSize = 10, string keyword = "", string sortBy = "")
        {
            List<Complain> complains = null;
            var _total = 0;

            if (String.IsNullOrEmpty(keyword))
            {
                switch (sortBy)
                {
                    case "ResolvedDate":
                        complains = db.Complains.OrderBy(x => x.ResolvedDate).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "Title":
                        complains = db.Complains.OrderBy(x => x.Title).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "ComplainStatusId":
                        complains = db.Complains.OrderBy(x => x.StatusId).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        complains = db.Complains.OrderBy(x => x.Id).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                }
                _total = db.Complains.Count();
            }
            else
            {
                keyword = keyword.ToLower();

                switch (sortBy)
                {
                    case "ResolvedDate":
                        complains = db.Complains.Where(c => c.Title.ToLower().Contains(keyword))
                    .OrderBy(x => x.ResolvedDate).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "Title":
                        complains = db.Complains.Where(c => c.Title.ToLower().Contains(keyword))
                     .OrderBy(x => x.Title).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "ComplainStatusId":
                        complains = db.Complains.Where(c => c.Title.ToLower().Contains(keyword))
                    .OrderBy(x => x.StatusId).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        complains = db.Complains.Where(c => c.Title.ToLower().Contains(keyword))
                   .OrderBy(x => x.Id).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                }

                _total = db.Complains.Count(c => c.Title.ToLower().Contains(keyword));
            }

            ComplainResult result = new ComplainResult();
            result.Complains = complains;
            result.CurrentPage = p;
            result.PageSize = pageSize;
            result.TotalComplains = _total;

            return View(result);
        }
        public ActionResult Create()
        {
            var locations = db.Locations.ToList();

            ViewBag.Locations = locations;
            return View();
        }

        [HttpPost]
        public ActionResult Create(int location_id, int computer_id, string title, string detail)
        {
            Complain c = new Complain();

            c.ComplainedBy = Convert.ToInt32(Session["uid"]);
            c.ComplainDate = DateTime.Now;
            c.StatusId = 1;
            c.Description = detail;
            c.Title = title;
            c.LocationId = location_id;
            c.ComputerId = computer_id;


            if (Request.Files != null && Request.Files[0] != null && !String.IsNullOrEmpty(Request.Files[0].FileName))
            {
                var file = Request.Files[0];
                var file_name = file.FileName;
                var file_path = Server.MapPath("~" + "\\Evidences\\");
                file.SaveAs(file_path + file_name);

                c.EvidenceFileName = file_name;
            }

            db.Complains.Add(c);
            db.SaveChanges();

            TempData["message"] = "Complain has been generated.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            var obj = db.Complains.Find(Id);
            db.Complains.Remove(obj);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}

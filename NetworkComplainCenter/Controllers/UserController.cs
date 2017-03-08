using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetworkComplainCenter.Models;

namespace NetworkComplainCenter.Controllers
{
    public class UserController : Controller
    {
        ComplainCenterEntities db = new ComplainCenterEntities();
        public ActionResult Index(int p = 0, int pageSize = 10, string keyword = "", string sortBy = "")
        {
            List<User> users = null;
            var _total = 0;

            if (String.IsNullOrEmpty(keyword))
            {
                switch (sortBy)
                {
                    case "Name":
                        users = db.Users.OrderBy(x => x.Name).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "Email":
                        users = db.Users.OrderBy(x => x.Email).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "UserTypeId":
                        users = db.Users.OrderBy(x => x.UserTypeId).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        users = db.Users.OrderBy(x => x.Id).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                }
                _total = db.Users.Count();
            }
            else
            {
                keyword = keyword.ToLower();

                switch (sortBy)
                {
                    case "Name":
                        users = db.Users.Where(c => c.Name.ToLower().Contains(keyword))
                    .OrderBy(x => x.Name).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "Email":
                        users = db.Users.Where(c => c.Name.ToLower().Contains(keyword))
                     .OrderBy(x => x.Email).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    case "UserTypeId":
                        users = db.Users.Where(c => c.Name.ToLower().Contains(keyword))
                    .OrderBy(x => x.UserTypeId).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                    default:
                        users = db.Users.Where(c => c.Name.ToLower().Contains(keyword))
                   .OrderBy(x => x.Id).Skip(p * pageSize).Take(pageSize).ToList();
                        break;
                }

                _total = db.Users.Count(c => c.Name.ToLower().Contains(keyword));
            }

            UserResult result = new UserResult();
            result.Users = users;
            result.CurrentPage = p;
            result.PageSize = pageSize;
            result.TotalUsers = _total;

            return View(result);
        }

        public ActionResult Create()
        {
            var usertypes = db.UserTypes.ToList();

            ViewBag.UserTypes = usertypes;

            return View();
        }
        [HttpPost]
        public ActionResult Create(string name, string email, string password, int usertype_id)
        {
            User u = new User();

            u.Name = name;
            u.Email = email;
            u.Password = password;
            u.UserTypeId = usertype_id;

            db.Users.Add(u);
            db.SaveChanges();

            TempData["message"] = "User added.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            var obj = db.Users.Find(Id);
            db.Users.Remove(obj);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

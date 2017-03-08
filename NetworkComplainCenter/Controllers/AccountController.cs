using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkComplainCenter.Controllers
{
    public class AccountController : Controller
    {
        ComplainCenterEntities db = new ComplainCenterEntities(); //database ka reference.//global rakha hay, not recommended.

        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            if (Request.Cookies["rememberme"] != null) // agar cookie humain request may mili hay.
            {
                var id = Convert.ToInt32(Request.Cookies["rememberme"].Value); // cookie say value read kari 

                var user = db.Users.FirstOrDefault(u => u.Id == id); // database may check kari

                if (user != null) //agar user mojood hay iss id kay against may.
                {
                    //session may values save kareen.
                    Session["email"] = user.Email;
                    Session["uid"] = user.Id;
                    Session["ut"] = user.UserType.Name;
                    Session["name"] = user.Name;
                    return RedirectToAction("Index", "Home"); // or phir admin home per redirect kar dia.
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(string email, string password, bool rememberme = false)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null) //agar email / password match nahin kartay tu 'user' null hota hay.
            {
                ViewBag.Error = "Invalid email/password";
                return View("Index");
            }
            else
            {
                //session banaya.
                Session["email"] = user.Email;
                Session["uid"] = user.Id;
                Session["ut"] = user.UserType.Name;
                Session["name"] = user.Name;

                if (rememberme)//agar user nay kaha hay kay mujhay yaad rakhna. tu phir cookie bana kar browser may save kara do.
                {
                    Response.Cookies.Add(new HttpCookie("rememberme")
                    {
                        Domain = "localhost",
                        Expires = DateTime.Now.AddDays(1),
                        Name = "rememberme",
                        Value = user.Id.ToString()
                    });
                } //warna cookie remove kar do.
                else
                {
                    Response.Cookies.Remove("rememberme");
                }
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Account");
        }

    }
}
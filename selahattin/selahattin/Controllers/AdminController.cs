using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using selahattin.Models;
namespace selahattin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        SelahattinBlogEntities ent = new SelahattinBlogEntities();
        public ActionResult Index()
        {
            ViewBag.post = ent.blog.Count();
            ViewBag.proje = ent.projects.Count();
         
            return View();
        }
      
    }
}
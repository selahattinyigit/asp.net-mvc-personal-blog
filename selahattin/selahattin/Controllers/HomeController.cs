using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using selahattin.Models;
namespace selahattin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        SelahattinBlogEntities ent = new SelahattinBlogEntities();
        [Route("")]
        [Route("Anasayfa")]
        public ActionResult Index()
        {
            ViewBag.hizmet = ent.siteIdentity.ToList();
            ViewBag.list = ent.blog.ToList();

            return View();
        }
        [Route("Hakkımda")]
        public ActionResult About()
        {
            ViewBag.hizmet = ent.siteIdentity.ToList();
            ViewBag.about = ent.aboutMe.ToList();

            return View();
        }

        [Route("Blog/{title}-{id:int}")]
        public ActionResult blogDetay(int id)
        {
            ViewBag.hizmet = ent.siteIdentity.ToList();
            ViewBag.blog = ent.blog.Where(db => db.blogId == id).ToList();
                return View();
           
        }
        [Route("Projeler")]
        public ActionResult projects()
        {
            ViewBag.hizmet = ent.siteIdentity.ToList();
            ViewBag.projeler = ent.projects.ToList();

            return View();
        }
        [Route("Projeler/{title}-{id:int}")]
        public ActionResult projectsDetay(int id)
        {
           ViewBag.hizmet = ent.siteIdentity.ToList();
             ViewBag.proje = ent.projects.Where(db => db.id == id).ToList();
            return View();

        }
        [HttpPost]
      
        public ActionResult abone(string mail)
        {
            aboneler a = new aboneler();
            a.mail = mail;
            a.date =Convert.ToDateTime(DateTime.Now);
            ent.aboneler.Add(a);
            ent.SaveChanges();
            return RedirectToAction("Index", "Home");
            
        }
        [HttpPost]
        public ActionResult Login(string mail,string password)
        {
            
            if (mail == "selahattin50" && password == "50905090")
            {
                Session["admin"] = mail;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["testmsg"] = " Kullanıcı Adı veya Şifreniz Hatalı ";
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}
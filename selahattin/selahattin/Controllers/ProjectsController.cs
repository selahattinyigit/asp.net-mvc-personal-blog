using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using selahattin.Models;
namespace selahattin.Controllers
{
    public class ProjectsController : Controller
    {
        SelahattinBlogEntities ent = new SelahattinBlogEntities();
        // GET: Projects
        public ActionResult Index()
        {
            return View(ent.projects.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(projects proje, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                projects k = new projects();

                WebImage img = new WebImage(image.InputStream);
                FileInfo imginfo = new FileInfo(image.FileName);

                string imagename = image.FileName;
                img.Resize(500, 1000);
                img.Save("~/Uploads/projects/" + imagename);
                k.image = "Uploads/projects/" + imagename;

                k.title = proje.title;
                k.comment = proje.comment;
                k.category = proje.category;
                ent.projects.Add(k);
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proje);

        }
        public ActionResult Edit(int id)
        {
            var proje = ent.projects.Where(x => x.id == id).SingleOrDefault();
            return View(proje);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, projects proje, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var k = ent.projects.Where(x => x.id == id).SingleOrDefault();
                if (image != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/" + k.image)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + k.image));
                    }
                    WebImage img = new WebImage(image.InputStream);
                    FileInfo imginfo = new FileInfo(image.FileName);

                    string imagename = image.FileName;
                    img.Resize(500, 1000);
                    img.Save("~/Uploads/projects/" + imagename);
                    k.image = "Uploads/projects/" + imagename;

                }

                k.comment = proje.comment;
                k.title = proje.title;
                k.category = proje.category;
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proje);
        }
        public ActionResult Delete(int id, projects proje)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            var b = ent.projects.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            ent.projects.Remove(b);
            ent.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
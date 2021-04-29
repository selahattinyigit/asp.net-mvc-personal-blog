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
    public class KimlikController : Controller
    {
        // GET: Kimlik
        SelahattinBlogEntities ent = new Models.SelahattinBlogEntities();
        public ActionResult Index()
        {
            return View(ent.siteIdentity.ToList());
        }
        public ActionResult Edit(int id)
        {
            var identity = ent.siteIdentity.Where(x => x.id == id).SingleOrDefault();
            return View(identity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, siteIdentity identity, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var k = ent.siteIdentity.Where(x => x.id == id).SingleOrDefault();
                if (image != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/" + k.image)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + k.image));
                    }
                    WebImage img = new WebImage(image.InputStream);
                    FileInfo imginfo = new FileInfo(image.FileName);

                    string imagename = image.FileName;
                    img.Resize(100, 200);
                    img.Save("~/Uploads/siteIdentity/" + imagename);
                    k.image = "Uploads/siteIdentity/" + imagename;

                }
                k.name = identity.name;
                k.comment = identity.comment;
                k.instagram = identity.instagram;
                k.linkedin = identity.linkedin;
                k.github = identity.github;
                k.stacoverflow = identity.stacoverflow;
                k.codepen = identity.codepen;
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(identity);
        }

    }
}
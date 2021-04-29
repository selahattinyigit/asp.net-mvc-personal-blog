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
    public class HakkimdaController : Controller
    {
        // GET: Hakkimda
        SelahattinBlogEntities ent = new Models.SelahattinBlogEntities();
        public ActionResult Index()
        {
            return View(ent.aboutMe.ToList());
        }
        public ActionResult Edit(int id)
        {
            var about = ent.aboutMe.Where(x => x.id == id).SingleOrDefault();
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, aboutMe about, HttpPostedFileBase image1, HttpPostedFileBase image2)
        {
            if (ModelState.IsValid)
            {
                var k = ent.aboutMe.Where(x => x.id == id).SingleOrDefault();
                if (image1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/" + k.image1)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + k.image1));
                    }
                    WebImage img = new WebImage(image1.InputStream);
                    FileInfo imginfo = new FileInfo(image1.FileName);

                    string imagename = image1.FileName;
                    img.Resize(1000, 460);
                    img.Save("~/Uploads/about/" + imagename);
                    k.image1 = "Uploads/about/" + imagename;

                }
                if (image2 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/" + k.image2)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + k.image2));
                    }
                    WebImage img = new WebImage(image2.InputStream);
                    FileInfo imginfo = new FileInfo(image2.FileName);

                    string imagename = image2.FileName;
                    img.Resize(1000, 460);
                    img.Save("~/Uploads/about/" + imagename);
                    k.image2 = "Uploads/about/" + imagename;

                }
                k.text1 = about.text1;
                k.text2 = about.text2;
                
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
        }
    }
}
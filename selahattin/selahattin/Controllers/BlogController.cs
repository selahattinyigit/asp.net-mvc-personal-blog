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
    public class BlogController : Controller
    {
        // GET: Blog
        SelahattinBlogEntities ent = new SelahattinBlogEntities();
        public ActionResult Index()
        {
            return View(ent.blog.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(blog blogs, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                blog k=new blog();
                 
                  WebImage img = new WebImage(image.InputStream);
                    FileInfo imginfo = new FileInfo(image.FileName);

                    string imagename = image.FileName;
                    img.Resize(500, 1000);
                    img.Save("~/Uploads/blog/" + imagename);
                    k.image = "Uploads/blog/" + imagename;

                
                k.link = blogs.link;
                k.text = blogs.text;
                k.title = blogs.title;
                ent.blog.Add(k);
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogs);
        
        }


        public ActionResult Edit(int id)
        {
            var blogs = ent.blog.Where(x => x.blogId == id).SingleOrDefault();
            return View(blogs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, blog blogs, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var k = ent.blog.Where(x => x.blogId == id).SingleOrDefault();
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
                    img.Save("~/Uploads/blog/" + imagename);
                    k.image = "Uploads/blog/" + imagename;

                }
                
                k.link = blogs.link;
                k.title = blogs.title;
                k.text = blogs.text;
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogs);
        }
        
        public ActionResult Delete(int id,blog blog)
        {
            if (id == null)
            {
                return HttpNotFound();

            }
            var b = ent.blog.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            ent.blog.Remove(b);
            ent.SaveChanges();
            return RedirectToAction("Index");

        }
      
    }
}
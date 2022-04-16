using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            var res = from k in db.Catgs
                      from p in db.Products
                      where k.CatgId == p.CatgId && k.Status==true
                      select new
                      { 
                          p.PId,
                          p.PName,
                          p.Price,
                          k.CatgName,
                      };
            List<CatgProductModel> plist = new List<CatgProductModel>();
            foreach(var r in res)
            {
                CatgProductModel cp = new CatgProductModel();
                cp.PId = r.PId;
                cp.PName = r.PName;
                cp.Price = (int)r.Price;
                
                cp.CatgName = r.CatgName;
                plist.Add(cp);
            }
            return View(plist);
        }

        // GET: Product/Details/5
        public CatgProductModel Details(int id)
        {
            CatgProductModel c =GetProductById(id);
            return (c);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            CatgProductModel cp = new CatgProductModel();

            var res = from k in db.Catgs select k;
            ViewBag.CatgId = new SelectList(res,"CatgId","CatgName");

            Product p = (from k in db.Products orderby k.PId descending select k).FirstOrDefault();
            int id = 1;
            if(p!=null)
            {
                id = p.PId + 1;
            }

            cp.PId = id;
           
            return View(cp);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(CatgProductModel cp)
        {
            try
            {
                // TODO: Add insert logic here
                Product p = new Product();
                p.PName = cp.PName;
                p.Price = cp.Price;
                p.CatgId = cp.CatgId;

                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public CatgProductModel GetProductById(int id)
        {
            var res = from c in db.Catgs
                      from p in db.Products
                      where c.CatgId == p.CatgId
                      && p.PId == id
                      select new
                      {
                          c.CatgName,
                          p.PId,
                          p.PName,
                          p.Price,
                          p.CatgId
                      };
            CatgProductModel cpm = new CatgProductModel();
            foreach(var r in res)
            {
                cpm.PId = r.PId;
                cpm.PName = r.PName;
                cpm.Price = (int)r.Price;
                cpm.CatgName = r.CatgName;
                cpm.CatgId = (int)r.CatgId;
            }
            return (cpm);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            CatgProductModel c = GetProductById(id);
            var rescatg = from k in db.Catgs
                          where k.Status == true
                          select k;
            ViewBag.anjali = new SelectList(rescatg, "CatgId", "CatgName", c.CatgId);
            return View(c);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,CatgProductModel c)
        {
            try
            {
                // TODO: Add update logic here
                Product p = (from k in db.Products
                             where k.PId == id
                             select k).FirstOrDefault();

                p.PName = c.PName;
                p.Price = c.Price;
                p.CatgId = c.CatgId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            CatgProductModel c = GetProductById(id);
            return View(c);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product t = (from p in db.Products
                             where p.PId == id
                             select p).FirstOrDefault();
                db.Products.Remove(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

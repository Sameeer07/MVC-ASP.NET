using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    
    public class CategoryController : Controller
    {
        // GET: Category
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            var res = from k in db.Catgs
                      where k.Status == true
                      select k;

            return View(res.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            Catg ctg = (from k in db.Catgs
                      where k.CatgId == id
                      select k).FirstOrDefault();

            return View(ctg);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            Catg ctg = new Catg();
            Catg ct = (from k in db.Catgs
                       orderby k.CatgId descending
                       select k).FirstOrDefault();
            int nextctgId = 0;
            if(ct==null)
            {
                nextctgId = 1;
            }
            else
            {
                nextctgId = ct.CatgId + 1;
            }
            ctg.CatgId = nextctgId;
            return View(ctg);
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Catg ctg)
        {
            try
            {
                // TODO: Add insert logic here
                db.Catgs.Add(ctg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            Catg ctg = (from k in db.Catgs
                        where k.CatgId == id
                        select k).FirstOrDefault();

            return View(ctg);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Catg ctg)
        {
            try
            {
                // TODO: Add update logic here
                Catg t = (from k in db.Catgs
                          where k.CatgId == id
                          select k).FirstOrDefault();
                if(t!=null)
                {
                    t.CatgName = ctg.CatgName;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            Catg t = (from k in db.Catgs
                      where k.CatgId == id
                      select k).FirstOrDefault();
            return View(t);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Catg ctg)
        {
            try
            {
                // TODO: Add delete logic here
                Catg p = (from k in db.Catgs
                          where k.CatgId == id
                          select k).FirstOrDefault();
                p.Status = false;
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

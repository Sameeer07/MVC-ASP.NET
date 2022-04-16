using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            var res = from k in db.Invoices
                      from c in db.Customers
                      where k.CId == c.CId
                      select new
                      {
                          k.InvId,
                          k.InvDate,
                          c.CName,
                          k.Total
                      };
            
            List<InvoiceModel> invlist = new List<InvoiceModel>();
            foreach(var r in res)
            {
                InvoiceModel t = new InvoiceModel();
                t.InvId = r.InvId;
                @ViewBag.InvDt = r.InvDate.ToString();
                t.InvDate1 = r.InvDate.ToString();
                t.CName = r.CName;
                t.Total = (int)r.Total;
               
                invlist.Add(t);

            }
            return View(invlist);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int id)
        {
            var res = from i in db.Invoices
                      from c in db.Customers
                      where i.InvId == id
                      && i.CId==c.CId
                      select new { i.InvId, i.InvDate, c.CName, c.CId,i.Total,c.Address, c.ContactNo };

            InvoiceModel im = new InvoiceModel();
            foreach(var r in res)
            {
                im.InvId = r.InvId;
                im.CName = r.CName;
                im.Total = (int)r.Total;
                im.Address = r.Address;
                im.ContactNo = r.ContactNo;
            }
            return View(im);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            var rescustlist = from c in db.Customers
                              select c;
            ViewBag.CId = new SelectList(rescustlist, "CId", "CName");
            Invoice nextinvid = (from v in db.Invoices
                                 orderby v.InvId descending
                                 select v).FirstOrDefault();
            int nextinv = 0;
            if(nextinvid==null)
            {
                nextinv = 1;
            }
            else
            {
                nextinv = nextinvid.InvId + 1;
            }
            DateTime aajkitarik = DateTime.Now;
            InvoiceModel imobj = new InvoiceModel();
            imobj.InvId = nextinv;
            imobj.InvDate = aajkitarik;

            var rescust = from c in db.Customers
                          select c;
            ViewBag.CId = new SelectList(rescust, "CId", "CName");
           

            return View(imobj);
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(InvoiceModel imobj)
        {
            try
            {
                // TODO: Add insert logic here
                Invoice t = new Invoice();
                t.InvDate = DateTime.Now;
                
                t.CId = imobj.CId;
                t.Total = imobj.Total;
                db.Invoices.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetOrders(int cid)
        {
            var res = from o in db.Orders
                      from p in db.Products
                      where o.CId == cid &&
                      o.PId == p.PId
                      select new { o.OId, p.PName, o.Price, o.Qty, o.Amt };
            List<InvoiceModel> ordlist = new List<InvoiceModel>();
            foreach (var r in res)
            {
                InvoiceModel om = new InvoiceModel();
                om.OId = r.OId;
                om.PName = r.PName;
                om.Price = (int)r.Price;
                om.Qty = (int)r.Qty;
                om.Amt = (int)r.Amt;
                ordlist.Add(om);
            }
            return Json(ordlist, JsonRequestBehavior.AllowGet);
          
        }

       
        public ActionResult GetCust(int cid)
        {
            Customer data = (from c in db.Customers
                             where c.CId == cid
                             select c).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

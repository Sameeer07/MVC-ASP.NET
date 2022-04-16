using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        Database1Entities db = new Database1Entities();
        public ActionResult Index()
        {
            var res = from o in db.Orders
                      from c in db.Customers
                      from p in db.Products
                      where o.CId == c.CId
                      && o.PId == p.PId
                      select new
                      {
                          o.OId,
                          p.Price,
                          o.Qty,
                          o.Amt,
                          c.CName,
                          p.PName
                      };
            List<OrderModel> olist = new List<OrderModel>();
            foreach(var r in res)
            {
                OrderModel om = new OrderModel();
                om.OId = r.OId;
                om.CName = r.CName;
                om.PName = r.PName;
                om.Price = (int)r.Price;
                om.Qty = (int)r.Qty;
                om.Amt = (int)r.Amt;
                olist.Add(om);

            }
            return View(olist);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            OrderModel om = GetDetails(id);
            return View(om);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var rescust = from c in db.Customers
                          select c;
            ViewBag.CId = new SelectList(rescust, "CId", "CName");
           
            var resprod = from p in db.Products
                          select p;
           
            ViewBag.PId = new SelectList(resprod, "PId", "PName");

            Order nextoid = (from o in db.Orders
                             orderby o.OId descending
                             select o).FirstOrDefault();
            int nextid = 0;
            if(nextoid==null)
            {
                nextid = 1;
            }
            else
            {
                nextid = nextoid.OId + 1;
            }
            OrderModel om = new OrderModel();
            om.OId = nextid;
            return View(om);
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(OrderModel om)
        {
            try
            {
                // TODO: Add insert logic here
                Order od = new Order();
                od.CId = om.CId;
                od.PId = om.PId;
                od.Price = om.Price;
                od.Qty = om.Qty;
                od.Amt = om.Amt;
                db.Orders.Add(od);
                var rescust = from c in db.Customers
                              select c;
                ViewBag.CId = new SelectList(rescust, "CId", "CName");
                var resprod = from p in db.Products
                              select p;
                ViewBag.PId = new SelectList(resprod, "PId", "PName");
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Detail(int id)
        {
            OrderModel om = GetDetails(id);
            return View(om);
        }
        

        public ActionResult GetPrice(int pid)
        {
            Product data = (from p in db.Products
                            where p.PId == pid
                            select p).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public OrderModel GetDetails(int id)
        {
            var res = from o in db.Orders
                      from c in db.Customers
                      from p in db.Products
                      where o.CId == c.CId
                      && o.PId == p.PId
                      && o.OId == id
                      select new { o.OId, p.Price, o.Qty, o.Amt, c.CName, p.PName, o.CId, o.PId };
            OrderModel om = new OrderModel();
            foreach(var r in res)
            {
                om.OId = r.OId;
                om.Price = (int)r.Price;
                om.Qty = (int)r.Qty;
                om.Amt = (int)r.Amt;
                om.CName = r.CName;
                om.PName = r.PName;
                om.CId = (int)r.CId;
                om.PId = (int)r.PId;
            }
            return om;
        }
        // GET: Order/Edit/5
        
        public ActionResult Edit(int id)
        {
            OrderModel om = GetDetails(id);
            var rescust = from c in db.Customers
                          select c;
            ViewBag.CId = new SelectList(rescust, "CId", "CName",om.CId);
            var resprod = from p in db.Products
                          select p;
            ViewBag.PId = new SelectList(resprod, "PId", "PName",om.PId);

            return View(om);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,OrderModel om)
        {
            try
            {
                // TODO: Add update logic here
                Order ord = (from k in db.Orders
                             where k.OId == id
                             select k).FirstOrDefault();
                ord.Price = om.Price;
                ord.Qty = om.Qty;
                ord.Amt = om.Amt;
                ord.CId = om.CId;
                ord.PId = om.PId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            OrderModel om = GetDetails(id);
            return View(om);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, OrderModel om)
        {
            try
            {
                // TODO: Add delete logic here
                Order od = (from o in db.Orders
                            where o.OId == id
                            select o).FirstOrDefault();
                db.Orders.Remove(od);
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

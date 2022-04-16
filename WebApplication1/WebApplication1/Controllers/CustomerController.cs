using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        Database1Entities db = new Database1Entities();
        public ActionResult CRUDoperation()
        {
            Customer c = new Customer();
            ViewBag.msg = " ";
            ViewBag.CId = 0;
            ViewBag.CName = " ";
            ViewBag.ContactNo = " ";
            ViewBag.Email = " ";
            ViewBag.Address = " ";
            Customer t = (from k in db.Customers
                          orderby k.CId descending
                          select k).FirstOrDefault();
            int nextcid = 0;
            if(t==null)
            {
                nextcid = 1;
            }
            else
            {
                nextcid = t.CId + 1;
            }
            c.CId = nextcid;
            return View(c);
        }
        [HttpPost]
        public ActionResult CRUDoperation(Customer c,string btn)
        {
            if(btn.Equals("save"))
            {
                db.Customers.Add(c);
                db.SaveChanges();
                ViewBag.msg = "data saved ";
            }
            else if(btn.Equals("search"))
            {
                Customer t = (from k in db.Customers
                              where k.CId == c.CId
                              select k).FirstOrDefault();
                if(t!=null)
                {
                    ViewBag.msg = t.CId;
                    ViewBag.CName = t.CName;
                    ViewBag.ContactNo = t.ContactNo;
                    ViewBag.Email = t.Email;
                    ViewBag.Address = t.Address;
                }
                else
                {
                    ViewBag.msg = "data not found";
                }
            }
            else if(btn.Equals("update"))
            {
                Customer t = (from k in db.Customers
                              where k.CId == c.CId
                              select k).FirstOrDefault();
                if(t!=null)
                {
                    t.CName = c.CName;
                    t.ContactNo = c.ContactNo;
                    t.Email = c.Email;
                    t.Address = c.Address;
                    db.SaveChanges();
                    ViewBag.msg = "data updated";
                }
                else
                {
                    ViewBag.msg = "data not updated";
                }
            }else if(btn.Equals("delete"))
            {
                Customer t = (from k in db.Customers
                              where k.CId == c.CId
                              select k).FirstOrDefault();
                if(t!=null)
                {
                    db.Customers.Remove(t);
                    db.SaveChanges();
                    ViewBag.msg = "data deleted";
                    ViewBag.msg = " ";
                    ViewBag.CId = 0;
                    ViewBag.CName = " ";
                    ViewBag.ContactNo = " ";
                    ViewBag.Email = " ";
                    ViewBag.Address = " ";
                }
                else
                {
                    ViewBag.msg = "data not deleted";
                }
            }
            return View();
        }
    }
}
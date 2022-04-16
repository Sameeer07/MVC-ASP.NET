using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class InvoiceModel
    {
        public int OId { get; set; }
        public int CId { get; set; }
        public int PId { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public int Amt { get; set; }
        public string CName { get; set; }
        public string PName { get; set; }
        public int InvId { get; set; }
        public DateTime InvDate{ get; set; }
        public int Total { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string InvDate1 { get; set; }
    }
}
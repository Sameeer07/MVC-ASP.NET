using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace WebApplication1.Models
{
    public class CatgProductModel
    {
        public int PId { get; set; }
        public string PName { get; set; }
        public int Price { get; set; }
        public int CatgId { get; set; }
        public string CatgName { get; set; }
    }
}
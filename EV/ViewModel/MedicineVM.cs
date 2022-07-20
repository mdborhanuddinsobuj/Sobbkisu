using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EV.ViewModel
{
    public class MedicineVM
    {
        public int pId { get; set; }
        public string pName { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
        public decimal total { get => qty * price; }
        public DateTime expiryDate { get; set; }
        public string pImage { get; set; }
        public string pStatus { get; set; }
        public string cName { get; set; }
    }
}
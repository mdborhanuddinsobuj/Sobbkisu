using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EV.CustomValidation;
using System.Linq;
using System.Web;

namespace EV.Models
{
    public class Medicines
    {
        [Key] 
        public int pId { get; set; }
        [Required, StringLength(50)]
        [DisplayName("Name")]
        public string pName { get; set; }
        [DisplayName("Qty")]
        public int qty { get; set; }
        [Required, DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal price { get; set; }
        [Required, DataType(DataType.Date)]
        [DisplayName("Expire Date")]
        [CustomExpireDate(ErrorMessage = "Expire date must be greater than Today's Date")]
        public DateTime expiryDate { get; set; }
        [Required]
        [DisplayName("Image")]
        public string pImage { get; set; }
        [Required]
        [DisplayName("Status")]
        public string pStatus { get; set; }
        [Required]
        [DisplayName("Category")]
        public int cId { get; set; }
        public virtual Categories Categories { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EV.Models
{
    public class Categories
    {
        public Categories()
        {
            this.Medicines = new HashSet<Medicines>();
        }

        [Required] 
        [Key]
        public int cId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string cName { get; set; }
        //nav
        public virtual ICollection<Medicines> Medicines { get; set; }
    }
}
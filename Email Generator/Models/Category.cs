using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Email_Generator.Models
{
    public class Category
    {
        [Display(Name = "ID"), Key]
        public int id { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Position")]
        public int? position { get; set; }
    }
}
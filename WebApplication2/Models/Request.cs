using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalNews.Models
{
    public class Request
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Peticion { get; set; }

        [Required]
        public string Empleado { get; set; }

        public bool Resuelta { get; set; }
    }

}

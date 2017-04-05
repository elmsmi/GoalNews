using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalNews.Models
{
    public class New 
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [AllowHtml]
        public string Noticia { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        public string Cliente { get; set; }
    }

    public class NoticiasViewModel
    {
        public New New { get; set; }
    }

}

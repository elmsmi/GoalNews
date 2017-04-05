using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoalNews.Models
{
    public class Client
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Cliente { get; set; }

    }

}
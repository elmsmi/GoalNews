using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoalNews.Models
{
    public class EmpCli
    {
        [Key]
        public int ID { get; set; }

        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        public int IDEmp { get; set; }

        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public int IDCli { get; set; }
    }
}
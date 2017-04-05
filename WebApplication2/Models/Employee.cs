using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoalNews.Models
{
    public class Employee
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Empleado { get; set; }

        [Required]
        public List<string> GetClients { get; set; }

        //public virtual ICollection<Client> Clients { get; set; }
    }

    public class Aux
    {
        public int empIdAux { get; set; }
        public string empAux { get; set; }
        public string cliAux { get; set; }

    }

}
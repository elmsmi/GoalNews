using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GoalNews.Models
{
    public class ContextClass: DbContext
    {
        public DbSet<New> News { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<EmpCli> EmpClis { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
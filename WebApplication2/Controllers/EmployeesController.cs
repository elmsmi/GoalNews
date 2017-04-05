using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoalNews.Models;

namespace GoalNews.Controllers
{
    public class EmployeesController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: Employees
        [Authorize]
        public ActionResult Index()
        {
            List<Aux> model = new List<Aux>();

            var empleados = db.Employees.ToList();
            var empcli = db.EmpClis.ToList();
            var clientes = db.Clients.ToList();
            var q = from x in empleados.AsEnumerable()
                    join y in empcli.AsEnumerable() on x.ID equals y.IDEmp
                    join z in clientes.AsEnumerable() on y.IDCli equals z.ID
                    select new { empA = x, cliA = z };

            foreach (var item in q)
            {
                model.Add(new Aux()
                {
                    empAux = item.empA.Empleado,
                    empIdAux = item.empA.ID,
                    cliAux = item.cliA.Cliente
                });
            }
            return View(model.OrderBy(p => p.empAux));
        }

        // GET: Employees/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Cliente = (from r in db.Clients select r.Cliente).Distinct();
            ViewBag.Empleado = (from r in db.Employees select r.Empleado).Distinct();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Empleado,GetClients")] Employee employee)
        {
            ViewBag.Cliente = (from r in db.Clients select r.Cliente).Distinct();

            int count = 0;
            if (employee.GetClients != null)
            {
                count = employee.GetClients.Count;
            }
            else
            {
                return View(employee);
            }

            // Creamos un nuevo empleado
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }

            // Creamos nuevas entradas que unen el Empleado con Clientes

            // Nuevo objeto que guardar
            EmpCli empCli = new EmpCli();

            // lo rellenamos con el ID del empleado 
            empCli.IDEmp = employee.ID;

            // lo rellenamos con los IDs de los Clientes => recorremos la lista GetClients y vamos guardando
            for (int i = 0; i < count; i++)
            {
                var client = employee.GetClients[i];
                empCli.IDCli = db.Clients.First(s => s.Cliente == client).ID;
                db.EmpClis.Add(empCli);
                db.SaveChanges();

                if (i == (count - 1))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Cliente = (from r in db.Clients select r.Cliente).Distinct();

            // Pasamos a la vista los Clientes de este empleado
            var aux = (from x in db.EmpClis where x.IDEmp == id select x);
            ViewBag.EClients = (from x in db.Clients join y in aux on x.ID equals y.IDCli select x).ToList();

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Empleado,GetClients")] Employee employee)
        {
            // Pasamos todos los clientes para el dropdown (para el caso de errores)
            ViewBag.Cliente = (from r in db.Clients select r.Cliente).Distinct();

            EmpCli empCli = new EmpCli();

            int count = 0;
            if (employee.GetClients != null)
            {
                count = employee.GetClients.Count;
            }
            else
            {
                return View(employee);
            }

            // Guardamos el Empleado modificado
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }

            // Aquí solo se añaden nuevos Clientes
            // TODO: implementar borrar Clientes
            empCli.IDEmp = employee.ID;
            for (int i = 0; i < count; i++)
            {
                var client = employee.GetClients[i];
                empCli.IDCli = db.Clients.First(s => s.Cliente == client).ID;
                db.EmpClis.Add(empCli);
                db.SaveChanges();

                if (i == (count - 1))
                {
                    return Redirect(Request.UrlReferrer.PathAndQuery);
                }
            }
            return View(employee);
        }

        // borramos una entrada de la tabla EmpCli
        // 
        [Authorize]
        public ActionResult DeleteFromEmpCli(int? IdC, int? IdE)
        {
            if (IdC == null || IdE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // sacamos el ID de la entrada
            var ID = (from x in db.EmpClis where x.IDCli == IdC && x.IDEmp == IdE select x.ID).First();

            EmpCli empcli = db.EmpClis.Find(ID);

            if (empcli == null)
            {
                return HttpNotFound();
            }

            db.EmpClis.Remove(empcli);
            db.SaveChanges();

            foreach (EmpCli ec in db.EmpClis)
            {
                db.EmpClis.Remove(ec);
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        // GET: Employees/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var ec = db.EmpClis.Where(x => x.IDEmp == id);
            db.EmpClis.RemoveRange(ec);
            db.SaveChanges();



            // Borramos el empleado de la tabla de empleado
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            foreach (Employee emp in db.Employees)
            {
                db.Employees.Remove(emp);
            }

            return RedirectToAction("Index");

            // borramos las entradas del empleado de la tabla EmpCLI
            // (sus clientes)

            //var ec = (from x in db.EmpClis where x.IDEmp == id select new { x });
            //db.EmpClis.RemoveRange(ec);

            //foreach (EmpCli c in db.EmpClis)
            //{
            //    db.EmpClis.Remove(c);
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

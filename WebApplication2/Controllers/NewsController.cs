using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoalNews.Models;

namespace GoalNews.Controllers
{
    public class NewsController : Controller
    {
        private ContextClass db = new ContextClass();

        // GET: News
        public ActionResult Index(string empleado, string cliente, string fecha)
        {
            ViewBag.Cliente = (from r in db.News select r.Cliente).Distinct();
            ViewBag.Fecha = (from r in db.News select r.Fecha).Distinct();
            ViewBag.Empleado = (from r in db.Employees select r.Empleado).Distinct();

            var dbNews = db.News.ToList();
            var dbClients = db.Clients.ToList();
            var dbEmpCli = db.EmpClis.ToList();

            if (fecha == "")
            {
                fecha = null;
            }
            if (empleado == "")
            {
                empleado = null;
            }
            if (cliente == "")
            {
                cliente = null;
            }

            if (empleado != null)
            {
                var IdEmpleado = (from x in db.Employees
                                  where x.Empleado == empleado || empleado == null || empleado == ""
                                  select new { x.ID }).FirstOrDefault().ID;

                var ClientesIdEmp = (from x in dbClients
                                     join y in dbEmpCli on x.ID equals y.IDCli
                                     where y.IDEmp == IdEmpleado
                                     select new { Clis = x.Cliente }).ToList();
                var newsE = (from x in dbNews join y in ClientesIdEmp on x.Cliente equals y.Clis
                             where x.Cliente == cliente || cliente == null || cliente == ""
                             where (x.Fecha.Date).ToString() == fecha || fecha == null || fecha == ""
                             select new NoticiasViewModel { New = x }).ToList();

                return View(newsE);

            }
            else
            {
                var news = (from x in dbNews
                            where x.Cliente == cliente || cliente == null || cliente == ""
                            where (x.Fecha.Date).ToString() == fecha || fecha == null || fecha == ""
                            select new NoticiasViewModel { New = x }).ToList();

                return View(news);

            }

        }

        public ActionResult ExportExcel()
        {
            return View(db.News.ToList());
        }

        // GET: News/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // GET: News/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Empleado = (from r in db.Employees select r.Empleado).Distinct();
            ViewBag.Cliente = (from r in db.Clients select r.Cliente).Distinct();
            return View();
        }

        // POST: News/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        //public ActionResult Create(New FormData)

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Noticia,Fecha,Cliente,Empleado")] New @new)
        {
            ViewBag.Cliente = (from r in db.News select r.Cliente).Distinct();
            if (ModelState.IsValid)
            {
                db.News.Add(@new);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        public ActionResult CreateEmployee([Bind(Include = "ID,Empleado")] New @new)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(@new);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@new);
        }

        // GET: News/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.Empleado = (from r in db.Employees select r.Empleado).Distinct();
            ViewBag.Cliente = (from r in db.Clients select r.Cliente).Distinct();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: News/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Noticia,Fecha,Cliente,Empleado")] New @new)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@new).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@new);
        }

        // GET: News/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = db.News.Find(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            New @new = db.News.Find(id);
            db.News.Remove(@new);
            db.SaveChanges();
            return RedirectToAction("Index");
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

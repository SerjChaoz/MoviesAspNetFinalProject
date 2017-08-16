using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoviesAspFinalProject.Models;

namespace MoviesAspFinalProject.Controllers
{
    public class RolesController : BaseController
    {
        // GET: Roles
        [AllowAnonymous]
        public ActionResult Index()
        {
            var roles = db.Roles.Include(r => r.Actor).Include(r => r.Movie);
            return View(roles.ToList());
        }

        // GET: Roles/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            ViewBag.ActorId = new SelectList(db.Actors.OrderBy(x => x.LastName), "ActorId", "FullName");
            ViewBag.MovieId = new SelectList(db.Movies.OrderByDescending(x => x.ReleaseYear), "MovieId", "Name");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleName,MovieId,ActorId")] Role role)
        {
            if (ModelState.IsValid)
            {
                Role checkrole = db.Roles.SingleOrDefault(x => x.ActorId == role.ActorId && x.MovieId == role.MovieId);
                if (checkrole == null)
                {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicated Role Detected");
                }
            }
            ViewBag.ActorId = new SelectList(db.Actors.OrderBy(x => x.LastName), "ActorId", "FullName", role.ActorId);
            ViewBag.MovieId = new SelectList(db.Movies.OrderByDescending(x => x.ReleaseYear), "MovieId", "Name", role.MovieId);
            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActorId = new SelectList(db.Actors.OrderBy(x => x.LastName), "ActorId", "FullName", role.ActorId);
            ViewBag.MovieId = new SelectList(db.Movies.OrderByDescending(x => x.ReleaseYear), "MovieId", "Name", role.MovieId);
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,RoleName,MovieId,ActorId")] Role role)
        {
            if (ModelState.IsValid)
            {
                Role tmprole = db.Roles.Find(role.RoleId);
                if (tmprole != null)
                {
                    Role checkmodel = db.Roles.SingleOrDefault(
                        x => x.RoleId != role.RoleId &&
                        x.MovieId == role.MovieId &&
                        x.ActorId == role.ActorId);
                    if (checkmodel == null)
                    {
                        tmprole.ActorId = role.ActorId;
                        tmprole.MovieId = role.MovieId;
                        tmprole.RoleName = role.RoleName;
                        tmprole.EditDate = DateTime.UtcNow;
                        db.Entry(tmprole).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Duplicated Role Detected");
                    }
                }
            }
            ViewBag.ActorId = new SelectList(db.Actors.OrderBy(x => x.LastName), "ActorId", "FullName", role.ActorId);
            ViewBag.MovieId = new SelectList(db.Movies.OrderByDescending(x => x.ReleaseYear), "MovieId", "Name", role.MovieId);
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}

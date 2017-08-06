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
    public class MoviesController : BaseController
    {

        // GET: Movies
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Movies.ToList().OrderByDescending(x => x.ReleaseYear));
        }

        // GET: Movies/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            Movie model = new Movie();
            ViewBag.Actors = new MultiSelectList(db.Actors.ToList().OrderBy(x => x.LastName), "ActorId", "FullName", model.Actors.Select(x => x.ActorId).ToArray());
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,ReleaseYear,Budget,Ids,RoleNames")] Movie movie, string[] Ids, string[] RoleNames)
        {
            if (ModelState.IsValid)
            {
                Movie checkmodel = db.Movies.SingleOrDefault(x => x.Name == movie.Name && x.ReleaseYear == movie.ReleaseYear);
                if (checkmodel == null)
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();

                    if (Ids != null)
                    {
                        for (int i = 0; i < Ids.Length; i++)
                        {
                            Actor actor = new Actor { ActorId = Ids[i] };
                            db.Actors.Attach(actor);
                            Role role = new Role();

                            role.Movie = movie;
                            role.Actor = actor;
                            role.RoleName = RoleNames[i];

                            db.Roles.Add(role);
                        }

                        db.Entry(movie).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicated Actor Detected");
                }
            }
            ViewBag.Actors = new MultiSelectList(db.Actors.ToList().OrderBy(x => x.LastName), "ActorId", "FullName", Ids);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Actors = new MultiSelectList(db.Actors.ToList().OrderBy(x => x.LastName), "ActorId", "FullName", movie.Actors.Select(x => x.ActorId).ToArray());
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Name,ReleaseYear,Budget,Ids,RoleNames")] Movie movie, string[] Ids, string[] RoleNames)
        {
            if (ModelState.IsValid)
            {
                Movie tmpmodel = db.Movies.Find(movie.MovieId);
                if (tmpmodel != null)
                {
                    Movie checkmodel = db.Movies.SingleOrDefault(
                        x => x.Name == movie.Name &&
                        x.ReleaseYear == movie.ReleaseYear &&
                        x.Budget == movie.Budget &&
                        x.MovieId != movie.MovieId);
                    if (checkmodel == null)
                    {
                        tmpmodel.Name = movie.Name;
                        tmpmodel.ReleaseYear = movie.ReleaseYear;
                        tmpmodel.Budget = movie.Budget;
                        tmpmodel.EditDate = DateTime.UtcNow;

                        db.Entry(tmpmodel).State = EntityState.Modified;

                        var removeItems = tmpmodel.Actors.Where(x => !Ids.Contains(x.ActorId)).ToList();

                        foreach (var removeItem in removeItems)
                        {
                            db.Entry(removeItem).State = EntityState.Deleted;
                        }

                        if (Ids != null)
                        {
                            for (int i = 0; i < Ids.Length; i++)
                            {
                                if (!tmpmodel.Actors.Select(x => x.ActorId).Contains(Ids[i]))
                                {
                                    Role role = new Role();
                                    role.MovieId = tmpmodel.MovieId;
                                    role.ActorId = Ids[i];
                                    role.RoleName = RoleNames[i];

                                    db.Roles.Add(role);
                                }
                                else if (tmpmodel.Actors.Single(x => x.ActorId == Ids[i]).RoleName != RoleNames[i])
                                {
                                    Role tmprole = db.Roles.Find(tmpmodel.Actors.Single(x => x.MovieId == Ids[i]).RoleId);
                                    tmprole.RoleName = RoleNames[i];
                                    tmprole.EditDate = DateTime.UtcNow;
                                    db.Entry(tmprole).State = EntityState.Modified;
                                }
                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Duplicated Movie Detected");
                    }
                }
            }
            ViewBag.Actors = new MultiSelectList(db.Actors.ToList().OrderBy(x => x.LastName), "ActorId", "FullName", Ids);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}

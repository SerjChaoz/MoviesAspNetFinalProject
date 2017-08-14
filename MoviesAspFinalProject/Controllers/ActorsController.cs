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
    public class ActorsController : BaseController
    {
        // GET: Actors
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Actors.ToList().OrderBy(x => x.LastName));
        }

        // GET: Actors/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: Actors/Create
        public ActionResult Create()
        {
            Actor model = new Actor();
            ViewBag.Movies = new MultiSelectList(db.Movies.ToList().OrderByDescending(x => x.ReleaseYear), "MovieId", "Name", model.Movies.Select(x => x.MovieId).ToArray());
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,BirthDay,DeathDay,Gender,HasOskar,Ids,RoleNames")] Actor model, string[] Ids, string[] RoleNames)
        {
            if (ModelState.IsValid)
            {
                bool validnames = true;
                foreach (string name in RoleNames)
                {
                    if (name == "")
                    {
                        validnames = false;
                    }
                }
                if (validnames)
                {
                    Actor checkmodel = db.Actors.SingleOrDefault(x => x.LastName == model.LastName
                                                && x.FirstName == model.FirstName && x.BirthDay == model.BirthDay);
                    if (checkmodel == null)
                    {
                        db.Actors.Add(model);
                        db.SaveChanges();

                        if (Ids != null)
                        {
                            for (int i = 0; i < Ids.Length; i++)
                            {
                                Movie movie = new Movie { MovieId = Ids[i] };
                                db.Movies.Attach(movie);
                                Role role = new Role();

                                role.Actor = model;
                                role.Movie = movie;
                                role.RoleName = RoleNames[i];

                                db.Roles.Add(role);
                            }

                            db.Entry(model).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Duplicated Actor Detected");
                    }
                }
            }
            ViewBag.Movies = new MultiSelectList(db.Movies.ToList().OrderByDescending(x => x.ReleaseYear), "MovieId", "Name", Ids);
            return View(model);
        }

        // GET: Actors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor model = db.Actors.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Movies = new MultiSelectList(db.Movies.ToList().OrderByDescending(x => x.ReleaseYear), "MovieId", "Name", model.Movies.Select(x => x.MovieId).ToArray());
            return View(model);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActorId,FirstName,LastName,BirthDay,DeathDay,Gender,HasOskar,Ids,RoleNames")] Actor model, string[] Ids, string[] RoleNames)
        {
            if (ModelState.IsValid)
            {
                bool validnames = true;
                foreach (string name in RoleNames)
                {
                    if (name == "")
                    {
                        validnames = false;
                    }
                }
                if (validnames)
                {
                    Actor tmpmodel = db.Actors.Find(model.ActorId);
                    if (tmpmodel != null)
                    {
                        Actor checkmodel = db.Actors.SingleOrDefault(
                            x => x.FirstName == model.FirstName &&
                            x.LastName == model.LastName &&
                            x.Gender == model.Gender &&
                            x.BirthDay == model.BirthDay &&
                            x.DeathDay == model.DeathDay &&
                            x.HasOskar == model.HasOskar &&
                            x.ActorId != model.ActorId);
                        if (checkmodel == null)
                        {
                            tmpmodel.FirstName = model.FirstName;
                            tmpmodel.LastName = model.LastName;
                            tmpmodel.Gender = model.Gender;
                            tmpmodel.BirthDay = model.BirthDay;
                            tmpmodel.DeathDay = model.DeathDay;
                            tmpmodel.HasOskar = model.HasOskar;
                            tmpmodel.EditDate = DateTime.UtcNow;

                            db.Entry(tmpmodel).State = EntityState.Modified;

                            var removeItems = (Ids == null ? tmpmodel.Movies.ToList() : tmpmodel.Movies.Where(x => !Ids.Contains(x.MovieId)).ToList());

                            foreach (var removeItem in removeItems)
                            {
                                db.Entry(removeItem).State = EntityState.Deleted;
                            }

                            if (Ids != null)
                            {
                                for (int i = 0; i < Ids.Length; i++)
                                {
                                    if (!tmpmodel.Movies.Select(x => x.MovieId).Contains(Ids[i]))
                                    {
                                        Role role = new Role();
                                        role.ActorId = tmpmodel.ActorId;
                                        role.MovieId = Ids[i];
                                        role.RoleName = RoleNames[i];

                                        db.Roles.Add(role);
                                    }
                                    else if (tmpmodel.Movies.Single(x => x.MovieId == Ids[i]).RoleName != RoleNames[i])
                                    {
                                        Role tmprole = db.Roles.Find(tmpmodel.Movies.Single(x => x.MovieId == Ids[i]).RoleId);
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
                            ModelState.AddModelError("", "Duplicated Actor Detected");
                        }
                    }
                }
            }
            ViewBag.Movies = new MultiSelectList(db.Movies.ToList().OrderByDescending(x => x.ReleaseYear), "MovieId", "Name", Ids);
            return View(model);
        }

        // GET: Actors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Actor model = db.Actors.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            foreach (var item in model.Movies.ToList())
            {
                db.Roles.Remove(item);
            }

            db.Actors.Remove(model);
            var deleted = db.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

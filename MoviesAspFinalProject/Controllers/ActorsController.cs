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
    public class ActorsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Actors
        public ActionResult Index()
        {
            return View(db.Actors.ToList());
        }

        // GET: Actors/Details/5
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
            ViewBag.Movies = new MultiSelectList(db.Movies.ToList(), "MovieId", "Name", model.Movies.Select(x => x.MovieId).ToArray());
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Age,Gender,HasOskar,CreateDate,EditDate,MovieIds")] Actor model, string[] MovieIds)
        {
            if (ModelState.IsValid)
            {
                Actor checkmodel = db.Actors.SingleOrDefault(x => x.LastName == model.LastName && x.FirstName == model.FirstName);
                if (checkmodel == null)
                {
                    db.Actors.Add(model);
                    db.SaveChanges();

                    if (MovieIds != null)
                    {
                        foreach (string movieId in MovieIds)
                        {
                            Movie movie = new Movie { MovieId = movieId };
                            db.Movies.Attach(movie);
                            Role role = new Role();

                            role.Actor = model;
                            role.Movie = movie;

                            // you need rewrite next line to specify code for Role Name here and in the view
                            role.RoleName = "Han Solo";

                            db.Roles.Add(role);
                        }

                        // not sute about next line, check it later please
                        //model.EditDate = model.CreateDate;

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
            ViewBag.Movies = new MultiSelectList(db.Movies.ToList(), "MovieId", "Name", MovieIds);
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

            ViewBag.Movies = new MultiSelectList(db.Movies.ToList(), "MovieId", "Name", model.Movies.Select(x => x.MovieId).ToArray());
            return View(model);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActorId,FirstName,LastName,Age,Gender,HasOskar,CreateDate,EditDate, MovieIds")] Actor model, string[] MovieIds)
        {
            if (ModelState.IsValid)
            {
                Actor tmpmodel = db.Actors.Find(model.ActorId);
                if (tmpmodel != null)
                {
                    Actor checkmodel = db.Actors.SingleOrDefault(
                        x => x.FirstName == model.FirstName &&
                        x.LastName == model.LastName &&
                        x.Gender == model.Gender &&
                        x.Age == model.Age &&
                        x.HasOskar == model.HasOskar &&
                        x.ActorId != model.ActorId);
                    if (checkmodel == null)
                    {
                        tmpmodel.FirstName = model.FirstName;
                        tmpmodel.LastName = model.LastName;
                        tmpmodel.Gender = model.Gender;
                        tmpmodel.Age = model.Age;
                        tmpmodel.HasOskar = model.HasOskar;
                        tmpmodel.EditDate = DateTime.Now;

                        db.Entry(tmpmodel).State = EntityState.Modified;

                        var removeItems = tmpmodel.Movies.Where(x => !MovieIds.Contains(x.MovieId)).ToList();

                        foreach (var removeItem in removeItems)
                        {
                            db.Entry(removeItem).State = EntityState.Deleted;
                        }

                        if (MovieIds != null)
                        {
                            var addedItems = MovieIds.Where(x => !tmpmodel.Movies.Select(y => y.MovieId).Contains(x));
                            foreach (string addedItem in addedItems)
                            {
                                Role role = new Role();
                                role.ActorId = tmpmodel.ActorId;
                                role.MovieId = addedItem;

                                // you need rewrite next line to specify code for Role Name here and in the view
                                role.RoleName = "SomeRole";

                                db.Roles.Add(role);
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
            ViewBag.Movies = new MultiSelectList(db.Movies.ToList(), "MovieId", "Name", MovieIds);
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

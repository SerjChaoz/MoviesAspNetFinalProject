using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoviesAspFinalProject.Models;
using Microsoft.AspNet.Identity;

namespace MoviesAspFinalProject.Controllers
{
    public class RatingsController : BaseController
    {
       
        public Rating SetRating(string MovieId, int MovieRating)
        {
            Rating rating = new Rating();
            rating.MovieId = MovieId;
            rating.MovieRating = MovieRating;
            rating.UserId = User.Identity.GetUserId();
            Rating checkrating = db.Ratings.SingleOrDefault(x => x.MovieId == rating.MovieId && x.UserId == rating.UserId);
            if (checkrating == null)
            {
                db.Ratings.Add(rating);
            }
            else
            {
                checkrating.MovieRating = rating.MovieRating;
                checkrating.EditDate = DateTime.UtcNow;
                db.Entry(checkrating).State = EntityState.Modified;
            }
            db.SaveChanges();

            rating = db.Ratings.Include(x => x.Movie).Include(x => x.Movie.Ratings).Include(x => x.User)
                .SingleOrDefault(x=>x.RatingId == rating.RatingId);

            return rating;
        }

        [AllowAnonymous]
        public PartialViewResult RatingsControl(string MovieId)
        {
            Movie movie = db.Movies.Find(MovieId);
            return PartialView("_RatingsControl", movie);
        }
    }
}

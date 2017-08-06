using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoviesAspFinalProject.Models
{
    public class Constants
    {
        public static string ApplicationName = "ASP.NET Movie Database";
    }

    public class BaseModel
    {
        [Display(Name = "Create Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;
    }
}
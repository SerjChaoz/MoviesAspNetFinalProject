namespace MoviesAspFinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {

        [Key]
        public string RatingId { get; set; }

        [Required]
        [Display(Name = "User")]
        [StringLength(128)]
        public string UserId { get; set; }
        public virtual AspNetUser User { get; set; }

        [Required]
        [StringLength(128)]
        public string MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public decimal MovieRating { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; }
      
    }
}

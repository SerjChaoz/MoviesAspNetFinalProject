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
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
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

        [Display(Name = "Movie Rating")]
        public decimal MovieRating { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;
      
    }
}

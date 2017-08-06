namespace MoviesAspFinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Movie : BaseModel
    {
        public Movie()
        {
        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string MovieId { get; set; }

        [Required]
        [Display(Name = "Movie Name")]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Required]
        [Display(Name = "Budget")]
        [StringLength(128)]
        public string Budget { get; set; }
        

        [Display(Name = "Actors")]
        [InverseProperty("Movie")]
        public virtual ICollection<Role> Actors { get; set; } = new HashSet<Role>();

        [Display(Name = "Ratings")]
        [InverseProperty("Movie")]
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        [NotMapped]
        public decimal OverallRating
        {
            get
            {
                if (Ratings.Count > 0)
                {
                    return Ratings.Average(x => x.MovieRating);
                }
                return 0;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}

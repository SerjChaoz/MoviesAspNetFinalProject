namespace MoviesAspFinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Movie
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

        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Required]
        [Display(Name = "Budget")]
        [StringLength(128)]
        public string Budget { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Actors")]
        [InverseProperty("Movie")]
        public virtual ICollection<Role> Actors { get; set; } = new HashSet<Role>();

        [Display(Name = "Ratings")]
        [InverseProperty("Movie")]
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}

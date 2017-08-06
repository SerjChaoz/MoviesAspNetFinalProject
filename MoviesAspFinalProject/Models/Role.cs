namespace MoviesAspFinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role : BaseModel
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string RoleId { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        [StringLength(128)]
        public string RoleName { get; set; }
        
        [Required]
        [Display(Name = "Movie")]
        [StringLength(128)]
        public string MovieId { get; set; }

        [Required]
        [Display(Name = "Actor")]
        [StringLength(128)]
        public string ActorId { get; set; }

        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", RoleName);
        }
    }
}

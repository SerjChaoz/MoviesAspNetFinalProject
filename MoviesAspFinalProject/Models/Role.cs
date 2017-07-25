namespace MoviesAspFinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role
    {
        public string RoleId { get; set; }

        [Required]
        [StringLength(128)]
        public string RoleName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EditDate { get; set; }

        [Required]
        [StringLength(128)]
        public string MovieId { get; set; }

        [Required]
        [StringLength(128)]
        public string ActorId { get; set; }

        public virtual Actor Actor { get; set; }

        public virtual Movie Movy { get; set; }
    }
}

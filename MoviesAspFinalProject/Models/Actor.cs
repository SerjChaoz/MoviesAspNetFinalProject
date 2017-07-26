namespace MoviesAspFinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Actor
    {
        public Actor()
        {
        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption:DatabaseGeneratedOption.Identity)]
        public string ActorId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [StringLength(128)]
        public string Gender { get; set; }

        [Display(Name = "Has Oskar")]
        public bool HasOskar { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Roles")]
        [InverseProperty("Actor")]
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        public override string ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }
    }
}

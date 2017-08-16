namespace MoviesAspFinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Actor : BaseModel
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

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return ToString();
            }
        }

        [Required]
        [ValidDate]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDay { get; set; }

        [Required]
        [ValidDate]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Date of Death")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeathDay { get; set; } = DateTime.MinValue;

        [NotMapped]
        [Display(Name = "Age")]
        public string Age {
            get
            {
                DateTime now;
                int age;
                string result = "";

                if (DeathDay.CompareTo(BirthDay) < 0)
                {
                    now = DateTime.UtcNow;
                }
                else
                {
                    now = DeathDay;
                    result = " (Deceased)";
                }

                if (now.CompareTo(BirthDay) <= 0)
                {
                    return "";
                }

                age = now.Year - BirthDay.Year;

                if (now.Month == BirthDay.Month && now.Day == BirthDay.Day && DeathDay.CompareTo(BirthDay) < 0)
                {
                    result = " Happy Birthday!";
                }
                else if(now.Month < BirthDay.Month || now.Month == BirthDay.Month && now.Day < BirthDay.Day)
                {
                    age--;
                }
                
                result = age.ToString() + result;
                return result;
            }
        }

        [Required]
        [Display(Name = "Gender")]
        [StringLength(128)]
        public string Gender { get; set; }

        [Display(Name = "Has Oskar")]
        public bool HasOskar { get; set; }
        
        [Display(Name = "Movies")]
        [InverseProperty("Actor")]
        public virtual ICollection<Role> Movies { get; set; } = new HashSet<Role>();
        
        public override string ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }
    }

    internal class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Must be a valid date.");
            }
            if (DateTime.UtcNow.CompareTo((DateTime)value) < 0)
            {
                return new ValidationResult("Date cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}

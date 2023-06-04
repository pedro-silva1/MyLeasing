using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "Number must be between 8 and 10 digits")]
        public string Document { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "The field {0} can only contain {1} characters in length")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "The field {0} can only contain {1} characters in length")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(9,ErrorMessage = "Number cannot exceed 9 digits")]
        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }

        [StringLength(9, ErrorMessage = "Number cannot exceed 9 digits")]
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        public string Address { get; set; }
    }
}

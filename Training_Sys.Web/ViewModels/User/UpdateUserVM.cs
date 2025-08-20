using System.ComponentModel.DataAnnotations;

namespace Training_Sys.Web.ViewModels.User
{
    public class UpdateUserVM
    {

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required]
        public string Role { get; set; }

        internal List<string> Roles = new List<string> { "Admin", "Instructor", "Trainee" };
    }
}

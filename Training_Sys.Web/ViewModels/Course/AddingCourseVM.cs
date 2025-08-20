using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Training_Sys.Web.ViewModels.Course
{
    public class AddingCourseVM
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }
        public int InstructorId { get; set; }
        [ValidateNever]
        public List<string> instructors { get; set; }


    }
}

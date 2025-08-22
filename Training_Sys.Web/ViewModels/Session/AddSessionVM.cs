using System.ComponentModel.DataAnnotations;

namespace Training_Sys.Web.ViewModels.Session
{
    public class AddSessionVM
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }

        //public string CourseName {  get; set; }

        //public List<Course>
    }
}

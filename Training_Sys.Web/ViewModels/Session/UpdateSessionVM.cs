using System.ComponentModel.DataAnnotations;

namespace Training_Sys.Web.ViewModels.Session
{
    public class UpdateSessionVM
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}

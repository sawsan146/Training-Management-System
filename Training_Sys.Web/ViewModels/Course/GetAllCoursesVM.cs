using System.ComponentModel.DataAnnotations;

namespace Training_Sys.Web.ViewModels.Course
{
    public class GetAllCoursesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string InsName { get; set; }
        public int InsID { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Training_Sys.Web.ViewModels.Grade
{
    public class AddGradeVM
    {
        [Required]
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100")]
        public float Value { get; set; }

        [Required]
        public int TraineeId { get; set; }

        [Required]
        public int SessionId { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using TrainingSys.Core.Interface;

namespace Training_Sys.Web.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TrainingSys.Core.Interface;
using System.Threading.Tasks;

namespace Training_Sys.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IGradeRepository _gradeRepository;

        public HomeController(
            ICourseRepository courseRepository,
            IUserRepository userRepository,
            ISessionRepository sessionRepository,
            IGradeRepository gradeRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _gradeRepository = gradeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var coursesCount = (await _courseRepository.GetAllAsync()).Count();
            var usersCount = (await _userRepository.GetAllAsync()).Count();
            var sessionsCount = (await _sessionRepository.GetAllAsync()).Count();
            var gradesCount = (await _gradeRepository.GetAllAsync()).Count();

            ViewBag.CoursesCount = coursesCount;
            ViewBag.UsersCount = usersCount;
            ViewBag.SessionsCount = sessionsCount;
            ViewBag.GradesCount = gradesCount;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Training_Sys.Web.ViewModels.Session;
using TrainingSys.Core.Entities;
using TrainingSys.Core.Interface;

namespace Training_Sys.Web.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ICourseRepository _courseRepository;

        public SessionController(ISessionRepository sessionRepository, ICourseRepository courseRepository)
        {
            _sessionRepository = sessionRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionRepository.GetAllAsync(s => s.Course);
            var result = sessions.Select(s => new SessionVM
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                CourseName = s.Course.Name
            }).ToList();

            return View(result);
        }

        public async Task<IActionResult> Add()
        {
            var courses = await _courseRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSessionVM vm)
        {
            if (ModelState.IsValid)
            {
                var session = new Session
                {
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    CourseId = vm.CourseId
                };

                await _sessionRepository.AddAsync(session);
                return RedirectToAction(nameof(Index));
            }
            var courses = await _courseRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name", vm.CourseId);
            return View("Add",vm);
        }

        public async Task<IActionResult> Update(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id, s => s.Course);
            if (session == null) return NotFound();

            var vm = new UpdateSessionVM
            {
                Id = session.Id,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                CourseId = session.CourseId
            };

            var courses = await _courseRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name", session.CourseId);

            return View("Update",vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSessionVM vm)
        {
            if (ModelState.IsValid)
            {
                var session = await _sessionRepository.GetByIdAsync(vm.Id);
                if (session == null) return NotFound();

                session.StartDate = vm.StartDate;
                session.EndDate = vm.EndDate;
                session.CourseId = vm.CourseId;

                await _sessionRepository.UpdateAsync(session);
                return RedirectToAction(nameof(Index));
            }
            var courses = await _courseRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name", vm.CourseId);
            return View("Update",vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id, s => s.Course);
            if (session == null) return NotFound();

            var vm = new SessionVM
            {
                Id = session.Id,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                CourseName = session.Course.Name
            };

            return View("Delete", vm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sessionRepository.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

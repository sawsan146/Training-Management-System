using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Training_Sys.Web.ViewModels.Grade;
using TrainingSys.Core.Entities;
using TrainingSys.Core.Interface;

namespace Training_Sys.Web.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;

        public GradeController(IGradeRepository gradeRepository, IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            _gradeRepository = gradeRepository;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var grades = await _gradeRepository.GetAllAsync(
                g => g.Trainee,
                g => g.Session,
                g => g.Session.Course 
            );

            var result = grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                TraineeName = g.Trainee?.Name ?? "N/A",
                SessionInfo = g.Session != null && g.Session.Course != null
                    ? $"{g.Session.Course.Name} - {g.Session.StartDate:dd/MM/yyyy}"
                    : "N/A"
            }).ToList();

            return View(result);
        }

        public async Task<IActionResult> Add()
        {
            var trainees = await _userRepository.FindAsync(u => u.Role == "Trainee");
            var sessions = await _sessionRepository.GetAllAsync(s => s.Course);

            ViewBag.Trainees = new SelectList(trainees, "Id", "Name");
            ViewBag.Sessions = new SelectList(sessions, "Id", "StartDate");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGradeVM vm)
        {
            if (ModelState.IsValid)
            {
                var grade = new Grade
                {
                    Value = vm.Value,
                    TraineeId = vm.TraineeId,
                    SessionId = vm.SessionId
                };

                await _gradeRepository.AddAsync(grade);
                return RedirectToAction(nameof(Index));
            }

            var trainees = await _userRepository.GetAllAsync(u => u.Role == "Trainee");
            var sessions = await _sessionRepository.GetAllAsync(s => s.Course);

            ViewBag.Trainees = new SelectList(trainees, "Id", "Name", vm.TraineeId);
            ViewBag.Sessions = new SelectList(sessions, "Id", "StartDate", vm.SessionId);

            return View(vm);
        }

        public async Task<IActionResult> Update(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id, g => g.Trainee, g => g.Session);
            if (grade == null) return NotFound();

            var vm = new UpdateGradeVM
            {
                Id = grade.Id,
                Value = grade.Value,
                TraineeId = grade.TraineeId,
                SessionId = grade.SessionId
            };

            var trainees = await _userRepository.FindAsync(u => u.Role == "Trainee");
            var sessions = await _sessionRepository.GetAllAsync(s => s.Course);

            ViewBag.Trainees = new SelectList(trainees, "Id", "Name", grade.TraineeId);
            ViewBag.Sessions = new SelectList(sessions, "Id", "StartDate", grade.SessionId);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateGradeVM vm)
        {
            if (ModelState.IsValid)
            {
                var grade = await _gradeRepository.GetByIdAsync(vm.Id);
                if (grade == null) return NotFound();

                grade.Value = vm.Value;
                grade.TraineeId = vm.TraineeId;
                grade.SessionId = vm.SessionId;

                await _gradeRepository.UpdateAsync(grade);
                return RedirectToAction(nameof(Index));
            }

            var trainees = await _userRepository.GetAllAsync(u => u.Role == "Trainee");
            var sessions = await _sessionRepository.GetAllAsync(s => s.Course);

            ViewBag.Trainees = new SelectList(trainees, "Id", "Name", vm.TraineeId);
            ViewBag.Sessions = new SelectList(sessions, "Id", "StartDate", vm.SessionId);

            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id, g => g.Trainee, g => g.Session.Course);
            if (grade == null) return NotFound();

            var vm = new GradeVM
            {
                Id = grade.Id,
                Value = grade.Value,
                TraineeName = grade.Trainee.Name,
                SessionInfo = $"{grade.Session.Course.Name} - {grade.Session.StartDate:dd/MM/yyyy}"
            };

            return View("Delete",vm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            await _gradeRepository.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        } 
    }
    
}

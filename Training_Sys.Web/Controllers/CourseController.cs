using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Training_Sys.Infrastructure.Data;
using Training_Sys.Web.ViewModels.Course;
using TrainingSys.Core.Entities;
using TrainingSys.Core.Interface;

namespace Training_Sys.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;


        public CourseController(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index(string? name, string? category)
        {
            var courses = await _courseRepository.GetAllAsync(c => c.Instructor);

            if (!string.IsNullOrEmpty(name))
            {
                courses =await _courseRepository.SearchByNameAsync(name);

                   
            }

            if (!string.IsNullOrEmpty(category))
            {
                courses = await _courseRepository.SearchByCategoryAsync(category);
            }

            var coursesVM = courses.Select(c => new GetAllCoursesVM
            {
                Id = c.Id,
                Name = c.Name,
                Category = c.Category,
                InsName = c.Instructor.Name,
                InsID = c.InstructorID
            });
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_CoursesTable", coursesVM);
            }
            return View(coursesVM);
        }


        public async Task<IActionResult> AddCourse()
        {
            var users = await _userRepository.GetAllInstructorsAsync();

            ViewBag.Instructors = new SelectList(users, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(AddingCourseVM courseVM)
        {
            var users = await _userRepository.GetAllInstructorsAsync();

            if (ModelState.IsValid)
            {          

                var course = new Course
                {
                    Name = courseVM.Name,
                    Category = courseVM.Category,
                    InstructorID=courseVM.InstructorId,
                };

                await _courseRepository.AddAsync(course);

                return RedirectToAction(nameof(Index));

            }

            ViewBag.Instructors = new SelectList(users, "Id", "Name");
            return View(courseVM);
        }

        public async Task<IActionResult> UpdateCourse(int id)
        {
            var course= await _courseRepository.GetByIdAsync(id);
            if (course != null)
            {
                var users = await _userRepository.GetAllInstructorsAsync();

                ViewBag.Instructors = new SelectList(users, "Id", "Name");

                var courseVM = new UpdateCourseVM
                {
                    Name = course.Name,
                    Category = course.Category,
                    InstructorId = course.InstructorID

                };

                return View(courseVM);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseVM courseVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var course = new Course
                    {
                        Name = courseVM.Name,
                        Category = courseVM.Category,
                        InstructorID = courseVM.InstructorId,

                    };
                   await _courseRepository.UpdateAsync(course);
                   return RedirectToAction(nameof(Index));
                }

                catch(Exception ex){
                    return Content(ex.Message);
                }

            }

            var users = await _userRepository.GetAllInstructorsAsync();

            ViewBag.Instructors = new SelectList(users, "Id", "Name");
            return View();


        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id,c=>c.Instructor);
            if (course == null)
            {
                return NotFound();
            }
            var courseVm = new DeleteCourseVm
            {
                Id = id,
                Name = course.Name,
                Category = course.Category,
                InsName = course.Instructor.Name
            };
           

            return View(courseVm); 
        }

        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _courseRepository.DeleteByIdAsync(id);
                TempData["SuccessMessage"] = "Course deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting course: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

       


    }
}

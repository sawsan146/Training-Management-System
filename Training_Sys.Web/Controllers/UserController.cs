using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Training_Sys.Infrastructure.Repository;
using Training_Sys.Web.ViewModels.Course;
using Training_Sys.Web.ViewModels.User;
using TrainingSys.Core.Entities;
using TrainingSys.Core.Interface;

namespace Training_Sys.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            
            var Users= await _userRepository.GetAllAsync();

            var usersVM = Users.Select(
                u => new GetAllUsersVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                });

            return View(usersVM);
        }

        public IActionResult AddUser()
        {
            ViewData["Roles"] = new List<string> { "Admin", "Instructor", "Trainee" };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserVm newUser)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                { 
                    Name=newUser.Name,
                    Email=newUser.Email,
                    Role=newUser.Role.ToLower(),
       
                };

                await _userRepository.AddAsync(user);
                return RedirectToAction("Index");


            }
            return View(newUser);
        }

        public async Task<IActionResult> UpdateUser(int id)
        {
            ViewData["Roles"] = new List<string> { "Admin", "Instructor", "Trainee" };


            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {


                var userVM = new UpdateUserVM
                {
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,                  

                };

                return View(userVM);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserVM userVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User
                    {
                        Name = userVM.Name,
                        Email=userVM.Email,
                        Role=userVM.Role,

                    };
                    await _userRepository.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    return Content(ex.Message);
                }

            }

            ViewData["Roles"] = new List<string> { "Admin", "Instructor", "Trainee" };

            return View();


        }


        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id,u=>u.Courses);

            if (user == null)
            {
                return NotFound();
            }
            else if (user.Courses.Count()>0)
            {
                TempData["ErrorMessage"] = "Cannot delete user because they are assigned to courses.";
                return RedirectToAction("Index");
            }
         
            var UserVM = new DeleteUserVM
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
            };


            return View(UserVM);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _userRepository.DeleteByIdAsync(id);
                TempData["SuccessMessage"] = "User deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting user: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }


    }
}

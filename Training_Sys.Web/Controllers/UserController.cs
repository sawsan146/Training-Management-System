using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
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




    }
}

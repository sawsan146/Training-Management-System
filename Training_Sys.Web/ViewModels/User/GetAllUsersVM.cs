using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Training_Sys.Web.ViewModels.User
{
    public class GetAllUsersVM
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}

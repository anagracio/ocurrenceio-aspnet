using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ocurrenceio_aspnet.Models;
using System.Diagnostics;

namespace ocurrenceio_aspnet.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager) {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ElectAdmins() {
            // get a list of all existing users in the database
            var users = _userManager.Users;
            // create an array of all users (but not IdentityUser. just an object with the userId, username, email and roles)
            var usersList = new List<object>();
            // for each user, get the roles and add to the array
            foreach (var user in users) {
                // if the user is the default super admin, skip
                if (user.UserName == "joca@occurrence.io") continue;
                var roles = _userManager.GetRolesAsync(user).Result;
                usersList.Add(new {
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = roles
                });
            }
            // return the view with the array of users
            return View(usersList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult MakeAdmin(string id) {
            // get user with the id passed in the url
            var user = _userManager.FindByIdAsync(id).Result;
            // add the role "Admin" to the user
            _userManager.AddToRoleAsync(user, "Admin").Wait();
            // redirect to the ElectAdmins view
            return RedirectToAction("ElectAdmins");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RemoveAdmin(string id) {
            // get user with the id passed in the url
            var user = _userManager.FindByIdAsync(id).Result;
            // remove the role "Admin" from the user
            _userManager.RemoveFromRoleAsync(user, "Admin").Wait();
            // redirect to the ElectAdmins view
            return RedirectToAction("ElectAdmins");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Case4.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller

    {
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            if (email == "admin@gmail.com" && password == "admin123")
            {
                return View(); // Show a success page
            }

            ViewBag.Error = "Invalid login credentials.";
            return View("Login");
        }

        [Route("UpdateProfile")]
        public IActionResult UpdateProfile()
        {
            return View();
        }

        [Route("ManageCourses")]
        public IActionResult AddRemoveCourses()
        {
            return View();
        }

        [Route("ManageTrainers")]
        public IActionResult AddRemoveTrainers()
        {
            return View();
        }

        [Route("ManageLearners")]
        public IActionResult AddRemoveLearners()
        {
            return View();
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            return RedirectToAction("Home","Home");
        }
    }
}

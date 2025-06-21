using Microsoft.AspNetCore.Mvc;

namespace Case4.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("Home",Name ="Home")]
        public IActionResult Home()
        {
            return View();
        }

        [Route("AboutUs",Name="AboutUs")]
        public IActionResult AboutUs()
        {
            return View();
        }
        [Route("ContactUs",Name ="ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }
        [Route("Login",Name ="Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("AdminLogin", Name = "AdminLogin")]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [Route("TrainerLogin", Name = "TrainerLogin")]
        public IActionResult TrainerLogin()
        {
            return View();
        }
        [Route("LearnerLogin", Name = "LearnerLogin")]
        public IActionResult LearnerLogin()
        {
            return View();
        }

    }
}

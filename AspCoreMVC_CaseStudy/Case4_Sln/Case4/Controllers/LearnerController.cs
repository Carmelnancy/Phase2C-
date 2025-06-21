using Microsoft.AspNetCore.Mvc;

namespace Case4.Controllers
{
    [Route("Learner")]
    public class LearnerController : Controller
    {
        [Route("UpdateProfile")]
        public IActionResult UpdateProfile() => View();

        [Route("SearchContent")]
        public IActionResult SearchContent() => View();

        [Route("Logout")]
        public IActionResult Logout() => RedirectToAction("Home", "Home");
    }
}

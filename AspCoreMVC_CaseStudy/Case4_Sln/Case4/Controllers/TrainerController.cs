using Microsoft.AspNetCore.Mvc;

namespace Case4.Controllers
{
    [Route("Trainer")]
    public class TrainerController : Controller
    {
        [Route("UpdateProfile")]
        public IActionResult UpdateProfile() => View();

        [Route("ManageContent")]
        public IActionResult AddRemoveContent() => View();

        [Route("Logout")]
        public IActionResult Logout() => RedirectToAction("Home", "Home");
    }
}

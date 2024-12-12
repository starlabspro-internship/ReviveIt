using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class JobBoardController : Controller
    {
        [HttpGet("JobBoard")]
        public IActionResult JobBoard()
        {
            return View();
        }
    }
}

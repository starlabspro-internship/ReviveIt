using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("JobBoard")]
    public class JobBoardController : Controller
    {
        [HttpGet("JobBoard")]
        public IActionResult JobBoard()
        {
            return View();
        }
    }
}

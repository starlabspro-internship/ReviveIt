using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("JobBoard")]
    public class JobBoardController : Controller
    {
        [HttpGet("JobBoard")] // Maps to: https://localhost:7018/JobBoard/JobBoard
        public IActionResult JobBoard()
        {
            return View(); // Looks for a corresponding View file in Views/JobBoard/JobBoard.cshtml
        }
    }
}

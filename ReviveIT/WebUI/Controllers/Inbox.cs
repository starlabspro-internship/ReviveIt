using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("Inbox")]
    public class InboxController : Controller
    {
        public IActionResult Inbox()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

public class ProfileController : Controller
{
    public IActionResult Profile()
    {
        var userProfile = new UserProfileModel
        {
            Name = GetUserName() ?? "Unavailable",
            Email = GetUserEmail() ?? "Unavailable",
            ProfileType = "Guest"
        };
        return View(userProfile);
    }

    private string GetUserName()
    {
        return null; 
    }

    private string GetUserEmail()
    {
        return null; 
    }

    [HttpPost]
    public IActionResult UploadProfileImage(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            TempData["ProfileImage"] = "/images/" + file.FileName;
        }
        else
        {
            TempData["ProfileImageError"] = "No file selected!";
        }

        return RedirectToAction("Profile");
    }

    [HttpPost]
    public IActionResult ChangeProfileType(string profileType)
    {
        TempData["ProfileType"] = profileType;

        return RedirectToAction("Profile");
    }

}

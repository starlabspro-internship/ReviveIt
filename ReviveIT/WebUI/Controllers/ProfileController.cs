using Microsoft.AspNetCore.Mvc;

public class ProfileController : Controller
{
    // Simulate fetching the user's profile data (e.g., from a database)
    public IActionResult Profile()
    {
        var userProfile = new UserProfileModel
        {
            Name = GetUserName() ?? "Unavailable",  // Fallback to "Unavailable"
            Email = GetUserEmail() ?? "Unavailable",  // Fallback to "Unavailable"
            ProfileType = "Guest"  // Default to 'Guest'
        };
        return View(userProfile);
    }

    // Simulated method for getting the user's name
    private string GetUserName()
    {
        return null;  // Simulate a case where name is unavailable
    }

    // Simulated method for getting the user's email
    private string GetUserEmail()
    {
        return null;  // Simulate a case where email is unavailable
    }

    // Method to handle profile image upload
    [HttpPost]
    public IActionResult UploadProfileImage(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            // Store the file in the "wwwroot/images" directory (ensure this directory exists)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // For simplicity, let's assume the file's name is the path for the profile image
            TempData["ProfileImage"] = "/images/" + file.FileName;
        }
        else
        {
            TempData["ProfileImageError"] = "No file selected!";
        }

        // Redirect back to the Profile page to reflect the change
        return RedirectToAction("Profile");
    }

    // Handle Profile Type Change
    [HttpPost]
    public IActionResult ChangeProfileType(string profileType)
    {
        // Simulate saving the selected profile type to the user's profile data
        TempData["ProfileType"] = profileType;

        return RedirectToAction("Profile");
    }

}

public class UserProfileModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ProfileImage { get; set; } // Path to the uploaded image
    public string ProfileType { get; set; } // 'Guest', 'Professional', 'Company'
}

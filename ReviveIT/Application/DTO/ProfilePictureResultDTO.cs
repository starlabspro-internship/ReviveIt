namespace Application.DTO
{
    public class ProfilePictureResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public static ProfilePictureResultDTO Success(string? profilePictureUrl)
        {
            return new ProfilePictureResultDTO { IsSuccess = true, ProfilePictureUrl = profilePictureUrl };
        }

        public static ProfilePictureResultDTO Failure(string errorMessage)
        {
            return new ProfilePictureResultDTO { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}

namespace Application.DTO
{
    public class LoginResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public bool RedirectToProfile { get; set; }

        public static LoginResultDTO Success(string token, bool redirectToProfile)
        {
            return new LoginResultDTO { IsSuccess = true, Token = token, RedirectToProfile = redirectToProfile };
        }

        public static LoginResultDTO Failure(string errorMessage)
        {
            return new LoginResultDTO { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}
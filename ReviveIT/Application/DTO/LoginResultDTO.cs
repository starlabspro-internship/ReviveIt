namespace Application.DTO
{
    public class LoginResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public bool IsEmailNotConfirmed { get; set; }
        public bool RedirectToProfile { get; set; }
        public string? ReturnUrl { get; set; }

        public static LoginResultDTO Success(string token, bool redirectToProfile = false)
        {
            return new LoginResultDTO
            {
                IsSuccess = true,
                Token = token,
                IsEmailNotConfirmed = false,
                RedirectToProfile = redirectToProfile
            };
        }

        public static LoginResultDTO Failure(string errorMessage, bool isEmailNotConfirmed = false)
        {
            return new LoginResultDTO
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                IsEmailNotConfirmed = isEmailNotConfirmed
            };
        }
    }
}

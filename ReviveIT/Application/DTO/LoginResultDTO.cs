namespace Application.DTO
{
    public class LoginResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public bool IsEmailNotConfirmed { get; set; }
        public string returnUrl { get; set; }

        public static LoginResultDTO Success(string token)
        {
            return new LoginResultDTO
            {
                IsSuccess = true,
                Token = token,
                IsEmailNotConfirmed = false
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

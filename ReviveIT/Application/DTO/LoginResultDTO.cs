namespace Application.DTO
{
    public class LoginResultDTO
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public string Token { get; private set; }
        public string RedirectUrl { get; private set; }

        public static LoginResultDTO Success(string token, string redirectUrl)
        {
            return new LoginResultDTO { IsSuccess = true, Token = token, RedirectUrl = redirectUrl };
        }

        public static LoginResultDTO Failure(string errorMessage)
        {
            return new LoginResultDTO { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}

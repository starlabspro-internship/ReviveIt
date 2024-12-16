namespace Application.DTO
{
    public class CompleteProfileResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public CompleteProfileResultDto(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static CompleteProfileResultDto SuccessResult(string message) =>
            new CompleteProfileResultDto(true, message);

        public static CompleteProfileResultDto FailureResult(string message) =>
            new CompleteProfileResultDto(false, message);
    }
}
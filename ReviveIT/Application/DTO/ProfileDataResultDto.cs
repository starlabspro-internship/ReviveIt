namespace Application.DTO
{
    public class ProfileDataResultDto
    {
        public bool IsSuccess { get; }
        public object Data { get; }
        public string ErrorMessage { get; }

        private ProfileDataResultDto(bool isSuccess, object data, string errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static ProfileDataResultDto Success(object data) => new ProfileDataResultDto(true, data, null);
        public static ProfileDataResultDto Failure(string errorMessage) => new ProfileDataResultDto(false, null, errorMessage);
    }
}
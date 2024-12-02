namespace Application.DTO
{
    public class UserInfoResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        private UserInfoResultDto(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static UserInfoResultDto SuccessResult(object data)
        {
            return new UserInfoResultDto(true, null, data);
        }

        public static UserInfoResultDto ErrorResult(string message)
        {
            return new UserInfoResultDto(false, message);
        }
    }
}
namespace Application.DTO
{
    public class JobPostResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static JobPostResultDto SuccessResult<T>(T data, string message = "Success")
        {
            return new JobPostResultDto
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static JobPostResultDto FailureResult(string message)
        {
            return new JobPostResultDto
            {
                Success = false,
                Message = message,
                Data = null
            };
        }
    }
}
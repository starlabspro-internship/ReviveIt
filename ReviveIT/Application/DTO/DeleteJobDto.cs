namespace Application.DTO
{
    public class DeleteJobDto
    {
        public bool IsSuccessful { get; private set; } 
        public string Message { get; private set; }
        public object Data { get; private set; }
        public int StatusCode { get; private set; }

        private DeleteJobDto(bool isSuccessful, string message, object data, int statusCode)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public static DeleteJobDto Success(string message, object data = null, int statusCode = 200) =>
            new DeleteJobDto(true, message, data, statusCode);

        public static DeleteJobDto Failure(string message, int statusCode = 400) =>
            new DeleteJobDto(false, message, null, statusCode);
    }
}
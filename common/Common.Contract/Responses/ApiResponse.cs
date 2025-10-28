namespace Common.Contract.Responses
{
    public class ApiResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public int ErrorCode { get; set; } = 0;


        public static ApiResponse Success(string message = "")
            => new() { Succeeded = true, Message = message };

        public static ApiResponse Failure(string error)
            => new() { Succeeded = false, Errors = new() { error } };
    }

    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public int ErrorCode { get; set; } = 0;
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "")
            => new() { Succeeded = true, Data = data, Message = message };

        public static ApiResponse<T> Failure(string error)
            => new() { Succeeded = false, Errors = new() { error } };
    }
}




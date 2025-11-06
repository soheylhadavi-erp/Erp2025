namespace Common.Contract
{
    public class ApiResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public int ErrorCode { get; set; } = 0;
    }

    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public int ErrorCode { get; set; } = 0;
        public T? Data { get; set; }
    }
}




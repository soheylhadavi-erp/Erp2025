namespace Common.Application
{
    public class OperationResultDto
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = "";
        public List<string> Errors { get; set; } = [];
        public int ErrorCode { get; set; } = 0;
    }
    public class OperationResultDto<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = "";
        public List<string> Errors { get; set; } = [];
        public int ErrorCode { get; set; } = 0;
        public T? Data { get; set; }
        //public static OperationResultDto<T> Success(T data,string message = "Operation completed successfully")
        //    => new() { Succeeded = true, Message = message };

        //public static OperationResultDto<T> Failure(string error)
        //    => new() { Succeeded = false, Errors = new() { error } };

        //public static OperationResultDto<T> Failure(List<string> errors)
        //    => new() { Succeeded = false, Errors = errors };
    }
}

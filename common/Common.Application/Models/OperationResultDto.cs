namespace Common.Application.Models
{
    public class OperationResultDto
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = "";
        public List<string> Errors { get; set; } = [];
        public int ErrorCode { get; set; } = 0;
        public static OperationResultDto Success(string message = "Operation completed successfully")
            => new() { Succeeded = true, Message = message };

        public static OperationResultDto Failure(string error)
            => new() { Succeeded = false, Errors = new() { error } };

        public static OperationResultDto Failure(List<string> errors)
            => new() { Succeeded = false, Errors = errors };
    }
}

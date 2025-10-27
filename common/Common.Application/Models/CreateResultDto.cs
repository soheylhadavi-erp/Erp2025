namespace Common.Application.Models
{
    public class CreateResultDto
    {
        public Guid Id { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; } = "";
        public List<string> Errors { get; set; } = [];
        public int ErrorCode { get; set; } = 0;
        public static CreateResultDto Success(string message = "Operation completed successfully")
            => new() { Succeeded = true, Message = message };

        public static CreateResultDto Failure(string error)
            => new() { Succeeded = false, Errors = new() { error } };

        public static CreateResultDto Failure(List<string> errors)
            => new() { Succeeded = false, Errors = errors };
    }
}

namespace Common.Application
{
    public class ResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string[] Errors { get; set; } = [];
        public int ErrorCode { get; set; } = 0;
    }
}

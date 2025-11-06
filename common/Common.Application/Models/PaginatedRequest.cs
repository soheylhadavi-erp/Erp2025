namespace Common.Application
{
    public class PaginatedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SkipRecords { get; set; } = 0; // اضافه شدن Skip

        public const int MaxPageSize = 100;
        public const int DefaultPageSize = 10;
    }
}

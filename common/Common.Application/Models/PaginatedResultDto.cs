namespace Common.Application
{
    public class PaginatedResultDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int SkippedRecords { get; set; } // اضافه شدن

        // محاسبات با درنظر گرفتن Skip
        public int TotalPages => (int)Math.Ceiling((TotalCount - SkippedRecords) / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        // Property کمکی
        public int DisplayedCount => Items.Count;
        public int RemainingRecords => Math.Max(TotalCount - SkippedRecords - (PageNumber * PageSize), 0);
    }

}

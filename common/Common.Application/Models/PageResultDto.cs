namespace Common.Application
{
    public class PageResultDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}

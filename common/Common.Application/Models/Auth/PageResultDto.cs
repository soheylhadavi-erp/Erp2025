namespace Common.Application.Models.Auth
{
    public class PageResultDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}

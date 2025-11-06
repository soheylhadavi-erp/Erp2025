namespace Common.Domain
{
    public interface IBaseEntity : ISoftDelete
    {
        public Guid Id { get; set; }
    }
}

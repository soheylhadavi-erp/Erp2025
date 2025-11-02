namespace Common.Domain.Interfaces.Base
{
    public interface IBaseEntity : ISoftDelete
    {
        public Guid Id { get; set; }
    }
}

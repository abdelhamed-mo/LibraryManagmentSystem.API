namespace Domain.Entities
{
	public abstract class BaseType<TKey>
	{
        public TKey Id { get; set; }
    }
}

using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
	public interface IUnitOfWork
	{
		public Task<int> SaveChangesAsync();
		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseType<TKey>;

	}
}

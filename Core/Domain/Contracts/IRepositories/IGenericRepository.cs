using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
	public interface IGenericRepository<TEntity,TKey> where TEntity : BaseType<TKey>
	{
		public Task<TEntity> GetAsync(TKey Id);
		public Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false);
		public Task AddAsync(TEntity entity);
		public void Update(TEntity entity);
		public void Delete(TEntity entity);
		public Task<TEntity> GetAsync(Specifications<TEntity> specifications);
		public Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications);
		Task<int> CountAsync(Specifications<TEntity> specifications);
	}
}


namespace Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey>(StoreContext storeContext) :
		IGenericRepository<TEntity, TKey> where TEntity : BaseType<TKey>
	{
		#region Without Specifications

		public async Task AddAsync(TEntity entity)
		=> await storeContext.Set<TEntity>().AddAsync(entity);

		public void Update(TEntity entity)
		=> storeContext.Set<TEntity>().Update(entity);

		public void Delete(TEntity entity)
		=> storeContext.Set<TEntity>().Remove(entity);

		public async Task<TEntity?> GetAsync(TKey Id)
		=> await storeContext.Set<TEntity>().FindAsync(Id);

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false)
		=> TrackChanges ? await storeContext.Set<TEntity>().ToListAsync() :
			await storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

		#endregion

		#region With Specifications
		public async Task<TEntity?> GetAsync(Specifications<TEntity> specifications)
			=> await ApplySpecification(specifications).FirstOrDefaultAsync();

		public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
			=> await ApplySpecification(specifications).ToListAsync();

		public async Task<int> CountAsync(Specifications<TEntity> specifications)
			=> await ApplySpecification(specifications).CountAsync();

		private IQueryable<TEntity> ApplySpecification(Specifications<TEntity> specifications)
			=> SpecificationEvaluator.QueryBuilder<TEntity>(storeContext.Set<TEntity>(), specifications);
		#endregion

	}
}

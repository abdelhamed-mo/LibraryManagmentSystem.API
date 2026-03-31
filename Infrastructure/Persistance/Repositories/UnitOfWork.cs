
namespace Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		readonly StoreContext storeContext;
		readonly ConcurrentDictionary<string, object> storedRepositories;

		public UnitOfWork(StoreContext storeContext)
		{
			this.storeContext = storeContext;
			this.storedRepositories = new();
		}

		public async Task<int> SaveChangesAsync()
		=> await storeContext.SaveChangesAsync();

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseType<TKey>
		=> (GenericRepository<TEntity, TKey>)
			storedRepositories.GetOrAdd(typeof(TEntity).Name
				, _ => new GenericRepository<TEntity, TKey>(storeContext));

		// الليله اللى فوق دى عشان لما اجى اكريت ابجكت مره يتخزن ولما اجى احتاجه تانى
		// استدعيه بدل م اكريته من اول وجديد ونملى الميمورى ع الفاضى
	}
}

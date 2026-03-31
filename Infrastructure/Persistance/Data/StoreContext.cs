namespace Persistence.Data
{
	public class StoreContext(DbContextOptions<StoreContext> options) : IdentityDbContext<User>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<PublishingHouse> PublishingHouses { get; set; }
	}
}

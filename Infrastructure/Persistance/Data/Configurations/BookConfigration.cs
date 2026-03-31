using Domain.Entities.BookModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasOne(b => b.Author).WithMany(a=>a.Books).HasForeignKey(b => b.AuthorId);
			builder.HasOne(b => b.PublishingHouse).WithMany(a=>a.Books).HasForeignKey(b => b.PublishingHouseId);

			//builder.Property(b => b.Price).HasColumnType("decimal(18,2)");
			//builder.Property(b => b.RentPrice).HasColumnType("decimal(18,2)");
		}

	}
}

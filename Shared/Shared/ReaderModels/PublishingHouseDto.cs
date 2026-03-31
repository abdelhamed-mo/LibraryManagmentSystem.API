
namespace Shared.ReaderModels
{
	public record PublishingHouseDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public ICollection<BookDto> Books { get; set; } // Nav Prop
	}
}

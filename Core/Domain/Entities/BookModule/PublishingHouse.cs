namespace Domain.Entities.BookModule
{
	public class PublishingHouse : BaseType<Guid>
	{
		public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Book> Books { get; set; } // Nav Prop
	}
}

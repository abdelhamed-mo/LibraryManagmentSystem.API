
namespace Shared.ReaderModels
{
	public record BookDto
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public DateTime PublicationDate { get; set; }
		public string Category { get; set; }

		// After User
		// Comments 
		// Rate

		public decimal Price { get; set; }
		//public decimal RentPrice { get; set; }

		//public int QuantityForSale { get; set; }
		//public int QuantityForRent { get; set; }

		public string PublishingHouseName { get; set; }    // Nav Prop
		public Guid PublishingHouseId { get; set; }
		public string AuthorName { get; set; }  // Nav Prop
		public Guid AuthorId { get; set; }

	}
}

namespace Shared.AdminModels
{
	public record FullBookDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublicationDate { get; set; }
		public string Category { get; set; }

		public string PdfUrl { get; set; }
		public string PictureUrl { get; set; }
		// After User
		// Comments 
		// Rate

		//public decimal SellingPrice { get; set; }
		//public decimal RentPrice { get; set; }

		//public int QuantityForSale { get; set; }
		//public int QuantityForRent { get; set; }

		public string PublishingHouseName { get; set; }    // Nav Prop
		public Guid PublishingHouseId { get; set; }
		public string AuthorName { get; set; }  // Nav Prop
		public Guid AuthorId { get; set; }
	}
}

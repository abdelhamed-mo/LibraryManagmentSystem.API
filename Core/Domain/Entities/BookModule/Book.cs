namespace Domain.Entities.BookModule
{
	public class Book : BaseType<Guid>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublicationDate { get; set; }
		public string Category { get; set; }

		public string PictureUrl { get; set; }
		public string PdfUrl { get; set; } = string.Empty;
		// After User
		// Comments 
		// Rate

		//public decimal SellingPrice { get; set; }
		//public decimal RentPrice { get; set; }

		//public int QuantityForSale { get; set; }
		//public int QuantityForRent { get; set; }

		public PublishingHouse PublishingHouse { get; set; }    // Nav Prop
		public Guid PublishingHouseId { get; set; }
		public Author Author { get; set; }  // Nav Prop
		public Guid AuthorId { get; set; }


	}

}

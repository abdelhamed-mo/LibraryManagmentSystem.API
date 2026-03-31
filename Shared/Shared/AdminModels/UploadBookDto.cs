using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AdminModels
{
	public record UploadBookDto
	{
		//public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PublicationDate { get; set; }
		public string Category { get; set; }

		//public string PictureUrl { get; set; }
		//public string PdfUrl { get; set; }
		// After User
		// Comments 
		// Rate

		public IFormFile Pdf { get; set; }
		public IFormFile Picture { get; set; }

		public decimal Price { get; set; }
		//public decimal RentPrice { get; set; }

		//public int QuantityForSale { get; set; }
		//public int QuantityForRent { get; set; }

		//public string PublishingHouseName { get; set; }    // Nav Prop
		//public string AuthorName { get; set; }  // Nav Prop
		public Guid PublishingHouseId { get; set; }
		public Guid AuthorId { get; set; }
	}
}

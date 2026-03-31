using Microsoft.AspNetCore.Http;
using Shared.ReaderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AdminModels
{
	public record UpdateAuthorDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public Gender Gender { get; set; }
		public string Nationality { get; set; }
		public string Bio { get; set; }
		public IFormFile? Picture { get; set; }
		//public string PicturUrl { get; set; }
	}

}

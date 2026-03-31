using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Shared.AdminModels
{
	public record AddAuthorDto
	{
		//public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public Gender Gender { get; set; }
		public string Nationality { get; set; }
		public string Bio { get; set; }
        public IFormFile Picture { get; set; }
        //public string PicturUrl { get; set; }


    }
	public enum Gender
	{
		[EnumMember(Value = "Female")]
		Female = 0,
		[EnumMember(Value = "Male")]
		Male = 1,
	}
}

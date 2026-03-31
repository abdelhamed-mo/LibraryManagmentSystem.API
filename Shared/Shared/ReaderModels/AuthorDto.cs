
using System.Runtime.Serialization;

namespace Shared.ReaderModels
{
	public record AuthorDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string Gender { get; set; }
		public string Nationality { get; set; }
		public string Bio { get; set; }
		public string PicturUrl { get; set; }
        public ICollection<BookDto> Books { get; set; } // Nav Prop 
	}
	//public enum Gender
	//{
	//	[EnumMember(Value = "Female")]
	//	Female = 0,
	//	[EnumMember(Value = "Male")]
	//	Male = 1,
	//}
}

using System.Runtime.Serialization;

namespace Domain.Entities.BookModule
{
	public class Author : BaseType<Guid>
	{
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public Gender Gender { get; set; }
		public string Nationality { get; set; }
        public string Bio { get; set; }
        public string PicturUrl { get; set; }

        public ICollection<Book> Books { get; set; } // Nav Prop 
	}

	public enum Gender
	{
		[EnumMember(Value = "Female")]
		Female = 0,
		[EnumMember(Value = "Male")]
		Male = 1,
	}
}

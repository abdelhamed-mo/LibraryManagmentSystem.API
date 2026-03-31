
namespace Service.MappingProfiles
{
	public class ReaderProfile : Profile
	{
		public ReaderProfile()
		{
			CreateMap<PublishingHouse, PublishingHouseDto>().ReverseMap();
			CreateMap<Author, AuthorDto>().ReverseMap();
			CreateMap<Book, BookDto>()
				.ForMember(a => a.AuthorName, opt => opt.MapFrom(a => a.Author.Name))
				.ForMember(h => h.PublishingHouseName, opt => opt.MapFrom(h => h.PublishingHouse.Name)).ReverseMap();
		}
	}
}

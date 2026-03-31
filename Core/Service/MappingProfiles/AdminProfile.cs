using Shared.AdminModels;

namespace Service.MappingProfiles
{
	public class AdminProfile : Profile
	{
		public AdminProfile()
		{
			CreateMap<Book, FullBookDto>()
				.ForMember(a => a.AuthorName, opt => opt.MapFrom(a => a.Author.Name))
				.ForMember(h => h.PublishingHouseName, opt => opt.MapFrom(h => h.PublishingHouse.Name)).ReverseMap();

		}
	}
}

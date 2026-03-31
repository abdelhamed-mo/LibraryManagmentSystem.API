namespace ServiceAbstraction
{
	public interface IReaderService
	{
		Task<PaginatedResultDto<BookDto>> GetAllBooksAsync(BooksParams _params);
		Task<BookDto> GetBookByIdAsync(Guid id);
		Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
		Task<AuthorDto> GetAuthorByIdAsync(Guid id);
		Task<IEnumerable<PublishingHouseDto>> GetAllPublishingHousesAsync();
		Task<PublishingHouseDto> GetPublishingHouseByIdAsync(Guid id);
	}
}

namespace ServiceAbstraction
{
	public interface IAdminService
	{
		#region Books
		Task<FullBookDto> GetBookById(Guid id);
		Task<PaginatedResultDto<FullBookDto>> GetAllBooks(BooksParams _params);

		Task<string> UploadBook(UploadBookDto book);
		Task<string> UpdateBook(UpdateBookDto book);
		Task<string> DeleteBook(Guid id);
		#endregion

		#region Authors
		Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
		Task<AuthorDto> GetAuthorByIdAsync(Guid id);
		
		Task<string> AddAuthor(AddAuthorDto publishingHouse);
		Task<string> UpdateAuthor(UpdateAuthorDto publishingHouse);
		Task<string> DeleteAuthor(Guid id);
		#endregion
		#region Publishing Houses
		Task<IEnumerable<PublishingHouseDto>> GetAllPublishingHousesAsync();
		Task<PublishingHouseDto> GetPublishingHouseByIdAsync(Guid id);

		Task<string> AddPublishingHouse(AddPublishingHouseDto author);
		Task<string> UpdatePublishingHouse(UpdatePublishingHouseDto author);
		Task<string> DeletePublishingHouse(Guid id);
		#endregion
	}
}

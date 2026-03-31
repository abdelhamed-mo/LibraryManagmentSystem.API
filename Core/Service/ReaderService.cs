namespace Service
{
	public class ReaderService(IUnitOfWork unitOfWork, IMapper mapper) : IReaderService
	{
		#region Books

		public async Task<PaginatedResultDto<BookDto>> GetAllBooksAsync(BooksParams _params)
		{
			var books = mapper.Map<IEnumerable<BookDto>>(await unitOfWork
					.GetRepository<Book, Guid>().GetAllAsync(new BookSpecifications(_params)));

			var totalCount = await unitOfWork
					.GetRepository<Book, Guid>().CountAsync(new BooksPaginatedResultSpecifications(_params));

			return new PaginatedResultDto<BookDto>(
				books.Count(),
				_params.PageIndex,
				totalCount,
				books);
		}
		public async Task<BookDto> GetBookByIdAsync(Guid id)
		{
			var book = await unitOfWork
					.GetRepository<Book, Guid>()
					.GetAsync(new BookSpecifications(id));

			return book is null
				? throw new BookNotFoundException(id)
				: mapper.Map<BookDto>(book);
		}
		#endregion

		#region Authors
		public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
		=> mapper.Map<IEnumerable<AuthorDto>>(await unitOfWork
			.GetRepository<Author, Guid>().GetAllAsync());
		public async Task<AuthorDto> GetAuthorByIdAsync(Guid id)
		{
			var author = await unitOfWork.GetRepository<Author, Guid>().GetAsync(id);
			return author is null
				? throw new AuthorNotFoundException(id)
				: mapper.Map<AuthorDto>(author);
		}

		#endregion

		#region publishingHouse
		public async Task<IEnumerable<PublishingHouseDto>> GetAllPublishingHousesAsync()
		=> mapper.Map<IEnumerable<PublishingHouseDto>>(await unitOfWork
			.GetRepository<PublishingHouse, Guid>().GetAllAsync());

		public async Task<PublishingHouseDto> GetPublishingHouseByIdAsync(Guid id)
		{
			var publishingHouse = await unitOfWork.GetRepository<PublishingHouse, Guid>().GetAsync(id);
			return publishingHouse is null
				? throw new PublishingHouseNotFoundException(id)
				: mapper.Map<PublishingHouseDto>(publishingHouse);
		}

		#endregion
	}
}

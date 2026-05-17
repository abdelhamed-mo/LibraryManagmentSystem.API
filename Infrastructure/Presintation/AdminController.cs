namespace Presentation
{
	public class AdminController(IServiceManager serviceManager) : ApiBaseController
	{
		#region Books
		[HttpGet("Book")]
		public async Task<ActionResult<FullBookDto>> GetBook([FromQuery] Guid id)
			=> Ok(await serviceManager.AdminService.GetBookById(id));
		[HttpGet("Books")]
		public async Task<ActionResult<FullBookDto>> GetAllBook([FromQuery] BooksParams _params)
			=> Ok(await serviceManager.AdminService.GetAllBooks(_params));
		[HttpPost("UploadBook")]
		public async Task<ActionResult<string>> UploadBook([FromForm] UploadBookDto book)
			=> Ok(await serviceManager.AdminService.UploadBook(book));
		[HttpPut("UpdateBook")]
		public async Task<ActionResult<string>> UpdateBook([FromForm] UpdateBookDto book)
			=> Ok(await serviceManager.AdminService.UpdateBook(book));
		[HttpDelete("DeleteBook")]
		public async Task<ActionResult<string>> DeleteBook([FromQuery] Guid id)
			=> Ok(await serviceManager.AdminService.DeleteBook(id));
		#endregion

		#region Authors

		[HttpGet("Authors")]
		public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
		=> Ok(await serviceManager.AdminService.GetAllAuthorsAsync());
		[HttpGet("Author")]
		public async Task<ActionResult<AuthorDto>> GetAuthor([FromQuery] Guid id)
		=> Ok(await serviceManager.AdminService.GetAuthorByIdAsync(id));
		[HttpPost("AddAuthor")]
		public async Task<ActionResult<string>> AddAuthor([FromForm] AddAuthorDto author)
		=> Ok(await serviceManager.AdminService.AddAuthor(author));
		[HttpPut("UpdateAuthor")]
		public async Task<ActionResult<string>> UpdateAuthor([FromForm] UpdateAuthorDto author)
		=> Ok(await serviceManager.AdminService.UpdateAuthor(author));
		[HttpDelete("DeleteAuthor")]
		public async Task<ActionResult<string>> DeleteAuthor([FromQuery] Guid id)
		=> Ok(await serviceManager.AdminService.DeleteAuthor(id));
		#endregion

		#region Publishing Houses
		[HttpGet("PublishingHouses")]
		public async Task<ActionResult<IEnumerable<PublishingHouseDto>>> GetAllPublishingHouses()
		=> Ok(await serviceManager.AdminService.GetAllPublishingHousesAsync());
		[HttpGet("PublishingHouse")]
		public async Task<ActionResult<PublishingHouseDto>> GetPublishingHouse([FromQuery] Guid id)
		=> Ok(await serviceManager.AdminService.GetPublishingHouseByIdAsync(id));
		[HttpPost("AddPublishingHouse")]
		public async Task<ActionResult<string>> AddPublishingHouse([FromForm] AddPublishingHouseDto PublishingHouse)
		=> Ok(await serviceManager.AdminService.AddPublishingHouse(PublishingHouse));
		[HttpPut("UpdatePublishingHouse")]
		public async Task<ActionResult<string>> UpdatePublishingHouse([FromForm] UpdatePublishingHouseDto PublishingHouse)
		=> Ok(await serviceManager.AdminService.UpdatePublishingHouse(PublishingHouse));
		[HttpDelete("DeletePublishingHouse")]
		public async Task<ActionResult<string>> DeletePublishingHouse([FromQuery] Guid id)
		=> Ok(await serviceManager.AdminService.DeletePublishingHouse(id));
		#endregion



	}
}

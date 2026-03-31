using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.ReaderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
	public class ReaderController(IServiceManager ServiceManager) : ApiBaseController
	{
		[HttpGet("Books")]
		[Authorize]
		public async Task<ActionResult<PaginatedResultDto<BookDto>>> GetAllBooks([FromQuery] BooksParams _params)
		=> Ok(await ServiceManager.ReaderService.GetAllBooksAsync(_params));
		[HttpGet("Book")]
		public async Task<ActionResult<BookDto>> GetBook([FromQuery] Guid id)
		=> Ok(await ServiceManager.ReaderService.GetBookByIdAsync(id));
		[HttpGet("Authors")]
		public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
		=> Ok(await ServiceManager.ReaderService.GetAllAuthorsAsync());
		[HttpGet("Author")]
		public async Task<ActionResult<AuthorDto>> GetAuthor([FromQuery] Guid id)
		=> Ok(await ServiceManager.ReaderService.GetAuthorByIdAsync(id));
		[HttpGet("PublishingHouses")]
		public async Task<ActionResult<IEnumerable<PublishingHouseDto>>> GetAllPublishingHouses()
		=> Ok(await ServiceManager.ReaderService.GetAllPublishingHousesAsync());
		[HttpGet("PublishingHouse")]
		public async Task<ActionResult<PublishingHouseDto>> GetPublishingHouse([FromQuery] Guid id)
		=> Ok(await ServiceManager.ReaderService.GetPublishingHouseByIdAsync(id));
	}
}

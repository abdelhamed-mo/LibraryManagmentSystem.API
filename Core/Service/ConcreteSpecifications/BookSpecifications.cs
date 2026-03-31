
namespace Service.ConcreteSpecifications
{
	public class BookSpecifications : Specifications<Book>
	{
		// Get by id
		public BookSpecifications(Guid id) :
			base(b => b.Id == id)
		{
			AddInclude(b => b.Author);
			AddInclude(b => b.PublishingHouse);
		}
		// Get all 
		public BookSpecifications(BooksParams _params) : 
			base(b=>(!_params.AuthorId.HasValue || b.AuthorId == _params.AuthorId) &&
					(!_params.PublishingHouseId.HasValue || b.PublishingHouseId == _params.PublishingHouseId) &&
					(string.IsNullOrWhiteSpace(_params.Search) || b.Title.ToUpper().Contains(_params.Search!.ToUpper().Trim())))
		{
			AddInclude(b => b.Author);
			AddInclude(b => b.PublishingHouse);

			ApplyPagination(_params.PageSize, _params.PageIndex);
		}
	}
}

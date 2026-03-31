using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConcreteSpecifications
{
	public class BooksPaginatedResultSpecifications : Specifications<Book>
	{
		public BooksPaginatedResultSpecifications(BooksParams _params) :
		base(b => (!_params.AuthorId.HasValue || b.AuthorId == _params.AuthorId) &&
				(!_params.PublishingHouseId.HasValue || b.PublishingHouseId == _params.PublishingHouseId) &&
				(string.IsNullOrWhiteSpace(_params.Search) || b.Title.ToUpper().Contains(_params.Search!.ToUpper().Trim())))
		{ }
	}
}

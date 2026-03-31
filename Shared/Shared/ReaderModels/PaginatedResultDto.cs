using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ReaderModels
{
	public record PaginatedResultDto<TData>
		(int PageSize, int PageIndex, int Total, IEnumerable<TData> Data)
	{
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
	public class AuthorNotFoundException(Guid id)
			: NotFoundException($"Author with id ({id}) isn't found")
	{
	}
}

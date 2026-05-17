using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
	public class UserNotFoundException(string id) : NotFoundException($"User with id ({id}) isn't found")
	{
	}
}
